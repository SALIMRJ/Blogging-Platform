
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Blogging_Platform.Controllers;
using Blogging_Platform.Services.UserService;
using Blogging_Platform.Domain.Model.User;

namespace Blogging_Platform.Test.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task GetById_ValidId_ReturnsOkResult()
        {
            var userId = "b5874847-722d-4b76-94dc-eca78300f664";

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetByIdAsync(userId))
                           .ReturnsAsync(new UserModel());

            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "b5874847-722d-4b76-94dc-eca78300f664"), 
            }, "mock"));

            var controller = new UserController(userServiceMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext }
            };

            var result = await controller.GetById(userId);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }


        [Fact]
        public async Task GetById_InvalidId_ReturnsBadRequestResult()
        {
            var userId = "invalidUserId";
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.GetByIdAsync(userId))
                           .ThrowsAsync(new Exception("User not found"));


            var httpContext = new DefaultHttpContext();
            httpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "b5874847-722d-4b76-94dc-eca78300f664"),
            }, "mock"));

            var controller = new UserController(userServiceMock.Object)
            {
                ControllerContext = new ControllerContext { HttpContext = httpContext }
            };

            var result = await controller.GetById(userId);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User not found", badRequestResult.Value);
        }

    }
}
