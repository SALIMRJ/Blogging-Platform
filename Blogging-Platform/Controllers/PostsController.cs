using Blogging_Platform.Domain.Entity;
using Blogging_Platform.Domain.Model.Post;
using Blogging_Platform.Services.PostService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace Blogging_Platform.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController( IPostService postService)
        {
            _postService = postService;

        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var response = await _postService.GetByIdAsync(id, userId);
                if (response == null) throw new Exception();

                Log.Information("GetPost =>{@Message}", response);
                return Ok(response);
            }catch (Exception ex)
            {
                Log.Error("GetPost =>{@Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("Craete")]
        public async Task<IActionResult> Create(PostRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var response = await _postService.CreateAsync(model,userId);
                Log.Information("CraetePost =>{@Message}", response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("CraetePost =>{@Message}", ex.Message);

                return BadRequest(ex.Message);
            }
        }


        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(long id, PostRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var response = await _postService.UpdateAsync(id, model, userId);
                Log.Information("UpdatePost =>{@Message}", response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("UpdatePost =>{@Message}", ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await _postService.DeleteAsync(id);
                Log.Information("DeletePost =>{@Message}", "Success");

                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error("DeletePost =>{@Message}", ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var response =  await _postService.GetAllAsync(userId);
                Log.Information("GetAllPosts =>{@Message}", response);

                return Ok(response);
            }
            catch (Exception ex)
            {
                Log.Error("GetAllPosts =>{@Message}", ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}