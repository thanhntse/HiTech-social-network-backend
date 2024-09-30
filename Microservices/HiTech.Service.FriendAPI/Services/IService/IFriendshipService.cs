using HiTech.Service.FriendAPI.DTOs.Response;

namespace HiTech.Service.FriendAPI.Services.IService
{
    public interface IFriendshipService
    {
        Task<IEnumerable<FriendshipResponse>> GetAllFriendDetailAsync(int userId);
        Task<IEnumerable<UserResponse>> GetAllFriendAsync(int userId);
        Task<bool> DeleteAsync(int userId, int id);
        Task<bool> ToggleBlockAsync(int userId, int id);
        Task<bool> FriendshipExists(int id);
        Task<bool> FriendshipExists(int userId1, int userId2);
    }
}
