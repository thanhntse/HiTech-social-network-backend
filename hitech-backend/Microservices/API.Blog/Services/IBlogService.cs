using API.Blog.DTO.Request;
using API.Blog.Entities;

namespace API.Blog.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<Entities.Blog>> GetAllBlogsAsync();
        Task<Entities.Blog> GetBlogByIdAsync(int id);
        Task<Entities.Blog> CreateBlogAsync(BlogRequest request);
        Task<Entities.Blog> UpdateBlogAsync(int id, BlogRequest request);
        Task DeleteBlogAsync(int id);
    }
}
