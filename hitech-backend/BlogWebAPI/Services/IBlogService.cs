using BlogWebAPI.DTO.Request;
using BlogWebAPI.Entities;

namespace BlogWebAPI.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<Blog>> GetAllBlogsAsync();
        Task<Blog> GetBlogByIdAsync(int id);
        Task<Blog> CreateBlogAsync(BlogRequest request);
        Task<Blog> UpdateBlogAsync(int id, BlogRequest request);
        Task DeleteBlogAsync(int id);
    }
}
