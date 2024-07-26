using API.Blog.DTO.Request;
using API.Blog.DTO.Response;
using API.Blog.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Blog.Controllers
{
    [Route("blog/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        // GET: api/blog/comment/1
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentResponse>> GetComment(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            return Ok(comment);
        }

        // GET: api/blog/comment/all/1
        [HttpGet("all/{id}")]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> GetCommentOfBlog(int id)
        {
            var comments = await _commentService.GetAllCommentsByBlogId(id);
            return Ok(comments);
        }

        // POST: api/blog/comment
        [HttpPost]
        public async Task<ActionResult<CommentResponse>> Create([FromBody] CommentRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var createdComment = await _commentService.CreateCommentAsync(request);

            return Ok(createdComment);
        }

        // PUT: api/blog/comment/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CommentResponse>> Update(int id, [FromBody] CommentRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var cmtToUpdate = await _commentService.GetCommentByIdAsync(id);

            if (cmtToUpdate == null)
            {
                return NotFound();
            }

            var comment = await _commentService.UpdateCommentAsync(id, request);

            return Ok(comment);
        }

        // DELETE: api/blog/comment/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var cmtToDelete = await _commentService.GetCommentByIdAsync(id);

            if (cmtToDelete == null)
            {
                return NotFound();
            }

            return Ok(await _commentService.DeleteCommentAsync(id));
        }
    }
}
