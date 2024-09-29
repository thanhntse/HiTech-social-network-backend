using HiTech.Service.FriendAPI.DTOs.Response;

namespace HiTech.Service.FriendAPI.Services.IService
{
    public interface IFriendRequestService
    {
        Task<IEnumerable<FriendRequestResponse>> GetAllSentRequestsAsync(int userId);
        Task<IEnumerable<FriendRequestResponse>> GetAllReceivedRequestsAsync(int userId);
        Task<FriendRequestResponse?> GetByIDAsync(int id);
        Task<bool> CreateAsync(int senderId, int receiverId);
        Task<bool> DeleteAsync(int id);
        Task<bool> FriendRequestExists(int senderId, int receiverId);
        Task<bool> AcceptRequest(int id);
        Task<bool> DenyRequest(int id);
    }
}
