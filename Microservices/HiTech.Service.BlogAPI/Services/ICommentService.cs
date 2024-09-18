using HiTech.Service.BlogAPI.DTOs.Request;
using HiTech.Service.BlogAPI.DTOs.Response;
using HiTech.Service.BlogAPI.Entities;

namespace HiTech.Service.BlogAPI.Services
{
    public interface ICommentService
    {
        Task<CommentResponse> GetCommentByIdAsync(int id);
        Task<IEnumerable<CommentResponse>> GetAllCommentsByBlogId(int blogId);
        Task<CommentResponse> CreateCommentAsync(CommentRequest request);
        Task<CommentResponse> UpdateCommentAsync(int id, CommentRequest request);
        Task<bool> DeleteCommentAsync(int id);
    }
}
