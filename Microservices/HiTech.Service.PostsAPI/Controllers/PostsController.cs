using Microsoft.AspNetCore.Mvc;
using HiTech.Service.PostsAPI.Services.IService;
using HiTech.Shared.Controllers;
using HiTech.Service.PostsAPI.DTOs.Response;
using HiTech.Service.PostsAPI.DTOs.Request;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HiTech.Service.PostsAPI.Controllers
{
    [Route("api/hitech/posts")]
    [Authorize]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/hitech/posts
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<PostResponse>>>> GetPosts()
        {
            var posts = await _postService.GetAllWithImageAsync();
            return Ok(HiTechApi.ResponseOk(posts));
        }

        // GET: api/hitech/posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<PostResponse>>> GetPost(int id)
        {
            var post = await _postService.GetByIDWithImageAsync(id);

            var response = post != null
                ? HiTechApi.ResponseOk(post)
                : HiTechApi.ResponseNotFound();

            return post != null ? Ok(response) : NotFound(response);
        }

        // PUT: api/hitech/posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> PutPost(int id, PostRequest request)
        {
            if (!await _postService.PostExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            bool success = await _postService.UpdateAsync(id, request);

            if (success)
            {
                return NoContent();
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // POST: api/hitech/posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<PostResponse>>> PostPost(PostRequest request)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            var post = await _postService.CreateAsync(id, request);

            if (post == null)
            {
                return BadRequest(HiTechApi.ResponseBadRequest());
            }
            var response = HiTechApi.Response(201, "Created.", post);
            return CreatedAtAction("GetPost",
                new { id = response?.Data?.PostId }, response);
        }

        // DELETE: api/hitech/posts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeletePost(int id)
        {
            if (!await _postService.PostExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            bool success = await _postService.DeleteAsync(id);
            if (success)
            {
                return NoContent();
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }
    }
}
