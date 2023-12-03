
using System.Security.Claims;
using Blogging_Platform.Controllers;
using Blogging_Platform.Domain.Model.Post;
using Blogging_Platform.Services.PostService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Blogging_Platform.Test.Controllers
{
    public class PostsControllerTests
    {
        [Fact]
        public async Task GetById_ValidId_ReturnsOkResult()
        {
            var postId = 4;
            var userId = "b5874847-722d-4b76-94dc-eca78300f664";

            var postServiceMock = new Mock<IPostService>();
            postServiceMock.Setup(x => x.GetByIdAsync(postId, userId))
                           .ReturnsAsync(new PostModel());


            var controller = new PostsController( postServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, userId)
                        }, "mock"))
                    }
                }
            };

            var result = await controller.GetById(postId);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Create_ValidModel_ReturnsOkResult()
        {
            var postRequestModel = new PostRequestModel {
            Content="Test Content",
            Title="Test Title"
            };
            var userId = "b5874847-722d-4b76-94dc-eca78300f664";

            var postServiceMock = new Mock<IPostService>();
            postServiceMock.Setup(x => x.CreateAsync(postRequestModel, userId))
                           .ReturnsAsync(new PostModel());


            var controller = new PostsController( postServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, userId)
                        }, "mock"))
                    }
                }
            };

            var result = await controller.Create(postRequestModel);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Update_ValidIdAndModel_ReturnsOkResult()
        {
            var postId = 1;
            var postRequestModel = new PostRequestModel
            {
                Content = "Test Content",
                Title = "Test Title"
            };

            var userId = "b5874847-722d-4b76-94dc-eca78300f664";

            var postServiceMock = new Mock<IPostService>();
            postServiceMock.Setup(x => x.UpdateAsync(postId, postRequestModel, userId))
                           .ReturnsAsync(new PostModel());


            var controller = new PostsController(postServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, userId)
                        }, "mock"))
                    }
                }
            };

            var result = await controller.Update(postId, postRequestModel);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Delete_ValidId_ReturnsOkResult()
        {
            var postId = 4;

            var postServiceMock = new Mock<IPostService>();

            var controller = new PostsController( postServiceMock.Object);

            var result = await controller.Delete(postId);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult()
        {
            var userId = "b5874847-722d-4b76-94dc-eca78300f664";

            var postServiceMock = new Mock<IPostService>();
            postServiceMock.Setup(x => x.GetAllAsync(userId))
                           .ReturnsAsync(new List<PostModel>());


            var controller = new PostsController(postServiceMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.NameIdentifier, userId)
                        }, "mock"))
                    }
                }
            };

            var result = await controller.GetAll();

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }


    }
}