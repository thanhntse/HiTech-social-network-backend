using BlogWebAPI.DTO.Request;
using BlogWebAPI.Entities;
using BlogWebAPI.Repositories;

namespace BlogWebAPI.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;

        public BlogService(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Blog> CreateBlogAsync(BlogRequest request)
        {
            var tags = request.Tags;

            var blog = new Blog
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

        public Task<IEnumerable<Blog>> GetAllBlogsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Blog> GetBlogByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Blog> UpdateBlogAsync(int id, BlogRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
