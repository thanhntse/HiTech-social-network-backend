using API.Blog.DTO.Request;
using API.Blog.DTO.Response;
using API.Blog.Entities;

namespace API.Blog.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogResponse>> GetAllBlogsAsync();
        Task<BlogResponse> GetBlogByIdAsync(int id);
        Task<BlogResponse> CreateBlogAsync(BlogRequest request);
        Task<BlogResponse> UpdateBlogAsync(int id, BlogRequest request);
        Task<bool> DeleteBlogAsync(int id);
    }
}
