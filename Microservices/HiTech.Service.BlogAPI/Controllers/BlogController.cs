using HiTech.Service.BlogAPI.DTOs.Request;
using HiTech.Service.BlogAPI.DTOs.Response;
using HiTech.Service.BlogAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HiTech.Service.BlogAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        // GET: api/blog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogResponse>>> GetBlogs()
        {
            var blogs = await _blogService.GetAllBlogsAsync();
            return Ok(blogs);
        }

        // GET: api/blog/user/1
        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<BlogResponse>>> GetBlogsOfUser(int id)
        {
            var blogs = await _blogService.GetAllBlogsByAuthorIdAsync(id);
            return Ok(blogs);
        }

        // GET: api/blog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogResponse>> GetBlog(int id)
        {
            var blog = await _blogService.GetBlogByIdAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        // POST: api/blog
        [HttpPost]
        public async Task<ActionResult<BlogResponse>> Create([FromBody] BlogRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var createdBlog = await _blogService.CreateBlogAsync(request);

            return Ok(createdBlog);
        }

        // PUT: api/blog/5
        [HttpPut("{id}")]
        public async Task<ActionResult<BlogResponse>> Update(int id, [FromBody] BlogRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            var blogToUpdate = await _blogService.GetBlogByIdAsync(id);

            if (blogToUpdate == null)
            {
                return NotFound();
            }

            var blog = await _blogService.UpdateBlogAsync(id, request);

            return Ok(blog);
        }

        // DELETE: api/blog/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var blogToDelete = await _blogService.GetBlogByIdAsync(id);

            if (blogToDelete == null)
            {
                return NotFound();
            }

            return Ok(await _blogService.DeleteBlogAsync(id));
        }
    }
}
