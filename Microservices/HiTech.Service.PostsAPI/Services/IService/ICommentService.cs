using HiTech.Service.PostsAPI.DTOs.Request;
using HiTech.Service.PostsAPI.DTOs.Response;

namespace HiTech.Service.PostsAPI.Services.IService
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentReponse>> GetAllByPostIDAsync(int id);
        Task<CommentReponse?> GetByIDAsync(int id);
        Task<CommentReponse?> CreateAsync(string authorId, CommentRequest request);
        Task<bool> UpdateAsync(int id, CommentRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> CommentExists(int id);
    }
}
