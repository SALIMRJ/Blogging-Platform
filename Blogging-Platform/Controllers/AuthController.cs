using Blogging_Platform.Domain.Model.Auth;
using Blogging_Platform.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Blogging_Platform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase 
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Registration(AuthRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _authService.RegisterAsync(model);
                if (!response.IsAuthenticated) {
                    Log.Error("Registration =>{@Message}", response.Message);
                return Conflict(response.Message);
                }
                Log.Information("Registration =>{@Message}", response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Registration =>{@Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AuthLoginModle model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var response = await _authService.LoginAsync(model);
                if (!response.IsAuthenticated)
                {
                    Log.Error("Login =>{@Message}", response.Message);

                    return BadRequest(response.Message);
                }
                Log.Information("Login =>{@Message}", response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("Login =>{@Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
