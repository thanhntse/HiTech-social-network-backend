using HiTech.Service.PostsAPI.DTOs.Request;
using HiTech.Service.PostsAPI.DTOs.Response;

namespace HiTech.Service.PostsAPI.Services.IService
{
    public interface IPostService
    {
        Task<IEnumerable<PostResponse>> GetAllWithImageAsync();
        Task<PostResponse?> GetByIDWithImageAsync(int id);
        Task<PostResponse?> CreateAsync(string authorId, PostRequest request);
        Task<bool> UpdateAsync(int id, PostRequest request);
        Task<bool> DeleteAsync(int id);
        Task<bool> PostExists(int id);
    }
}
