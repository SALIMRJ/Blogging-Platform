using Blogging_Platform.Domain.Model.Auth;
using Blogging_Platform.Services.AuthService;
using Blogging_Platform.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Blogging_Platform.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var response = await _userService.GetByIdAsync(id);
                Log.Information("GetUserInfo =>{@Message}", response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("GetUserInfo =>{@Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
