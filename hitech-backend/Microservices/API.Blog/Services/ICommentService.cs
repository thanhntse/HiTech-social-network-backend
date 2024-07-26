using API.Blog.DTO.Request;
using API.Blog.DTO.Response;
using API.Blog.Entities;

namespace API.Blog.Services
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
