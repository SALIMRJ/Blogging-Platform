
using Blogging_Platform.Controllers;
using Blogging_Platform.Domain.Model.Auth;
using Blogging_Platform.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Blogging_Platform.Test.Controllers
{
    public class AuthControllerTests
    {
        [Fact]
        public async Task Registration_ValidModel_ReturnsOkResult()
        {
            var authRequestModel = new AuthRequestModel { 
            Email="RTest@test.com",
            FirstName="Rtest",
            LastName="RTest",
            Password="P@ssw0rd",
            UserName="RTest"
            };

            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(x => x.RegisterAsync(authRequestModel))
                           .ReturnsAsync(new AuthModel { IsAuthenticated = true });

            var controller = new AuthController(authServiceMock.Object);

            var result = await controller.Registration(authRequestModel);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Registration_InvalidModel_ReturnsBadRequestResult()
        {
            var authRequestModel = new AuthRequestModel
            {
                Email = "com",
                FirstName = "",
                LastName = null,
                Password = "1",
                UserName = ""
            };
            var authServiceMock = new Mock<IAuthService>();
            var controller = new AuthController(authServiceMock.Object);
            controller.ModelState.AddModelError("Key", "Error Message");

            var result = await controller.Registration(authRequestModel);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }

        [Fact]
        public async Task Login_ValidModel_ReturnsOkResult()
        {
            var authLoginModel = new AuthLoginModle { UserName="SALIM",Password="P@ssw0rd" };

            var authServiceMock = new Mock<IAuthService>();
            authServiceMock.Setup(x => x.LoginAsync(authLoginModel))
                           .ReturnsAsync(new AuthModel { IsAuthenticated = true });

            var controller = new AuthController(authServiceMock.Object);

            var result = await controller.Login(authLoginModel);

            Assert.IsType<OkObjectResult>(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task Login_InvalidModel_ReturnsBadRequestResult()
        {
            var authLoginModel = new AuthLoginModle { UserName = "", Password = "P" };

            var authServiceMock = new Mock<IAuthService>();
            var controller = new AuthController(authServiceMock.Object);
            controller.ModelState.AddModelError("Key", "Error Message");

            var result = await controller.Login(authLoginModel);

            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }
    }
}