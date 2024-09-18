using HiTech.Service.BlogAPI.DTOs.Request;
using HiTech.Service.BlogAPI.DTOs.Response;
using HiTech.Service.BlogAPI.Entities;

namespace HiTech.Service.BlogAPI.Services
{
    public interface IBlogService
    {
        Task<IEnumerable<BlogResponse>> GetAllBlogsAsync();
        Task<IEnumerable<BlogResponse>> GetAllBlogsByAuthorIdAsync(int authorId);
        Task<BlogResponse> GetBlogByIdAsync(int id);
        Task<BlogResponse> CreateBlogAsync(BlogRequest request);
        Task<BlogResponse> UpdateBlogAsync(int id, BlogRequest request);
        Task<bool> DeleteBlogAsync(int id);
    }
}
