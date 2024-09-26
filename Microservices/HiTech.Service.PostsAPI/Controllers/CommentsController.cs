using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using HiTech.Service.PostsAPI.Services.IService;
using HiTech.Service.PostsAPI.DTOs.Response;
using HiTech.Shared.Controllers;
using HiTech.Service.PostsAPI.DTOs.Request;
using System.Security.Claims;

namespace HiTech.Service.PostsAPI.Controllers
{
    [Route("api/hitech/comments")]
    [Authorize]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService, IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }

        // GET: api/hitech/comments/post/5
        [HttpGet("post/{id}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CommentReponse>>>> GetCommentsByPostID(int id)
        {
            if (!await _postService.PostExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            var comments = await _commentService.GetAllByPostIDAsync(id);
            return Ok(HiTechApi.ResponseOk(comments));
        }

        // GET: api/hitech/comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<CommentReponse>>> GetComment(int id)
        {
            var commnent = await _commentService.GetByIDAsync(id);

            if (commnent == null)
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }
            return Ok(HiTechApi.ResponseOk(commnent));
        }

        // PUT: api/hitech/comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse>> PutComment(int id, CommentRequest request)
        {
            if (!await _commentService.CommentExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            bool success = await _commentService.UpdateAsync(id, request);

            if (success)
            {
                return NoContent();
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }

        // POST: api/hitech/comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<CommentReponse>>> PostComment(CommentRequest request)
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (id == null)
            {
                return Unauthorized(HiTechApi.ResponseUnauthorized());
            }

            var comment = await _commentService.CreateAsync(id, request);

            if (comment == null)
            {
                return BadRequest(HiTechApi.ResponseBadRequest());
            }
            var response = HiTechApi.Response(201, "Created.", comment);
            return CreatedAtAction("GetComment",
                new { id = response?.Data?.CommentId }, response);
        }

        // DELETE: api/hitech/comment/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteComment(int id)
        {
            if (!await _commentService.CommentExists(id))
            {
                return NotFound(HiTechApi.ResponseNotFound());
            }

            bool success = await _commentService.DeleteAsync(id);
            if (success)
            {
                return NoContent();
            }
            return BadRequest(HiTechApi.ResponseBadRequest());
        }
    }
}
