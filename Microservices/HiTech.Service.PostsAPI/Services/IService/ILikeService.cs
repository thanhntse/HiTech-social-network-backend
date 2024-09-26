using HiTech.Service.PostsAPI.DTOs.Response;

namespace HiTech.Service.PostsAPI.Services.IService
{
    public interface ILikeService
    {
        Task<IEnumerable<UserResponse>> GetAllUserByPostIDAsync(int postId);
        Task<bool> CreateAsync(string authorId, int postId);
        Task<bool> DeleteAsync(string authorId, int postId);
        Task<bool> LikeExists(string authorId, int postId);
    }
}
