using API.Blog.DTO.Request;
using API.Blog.Entities;
using API.Blog.Repositories;

namespace API.Blog.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Entities.Blog> CreateBlogAsync(BlogRequest request)
        {
            var tags = request.Tags;

            var blog = new Entities.Blog
            {
                AuthorId = request.AuthorId,
                Title = request.Title,
                Content = request.Content,
            };

            await _blogRepository.AddAsync(blog);
            return blog;
        }

        public Task DeleteBlogAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Entities.Blog>> GetAllBlogsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Entities.Blog> GetBlogByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Entities.Blog> UpdateBlogAsync(int id, BlogRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
