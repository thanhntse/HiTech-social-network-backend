using HiTech.Service.FriendAPI.DTOs.Response;

namespace HiTech.Service.FriendAPI.Services.IService
{
    public interface IFriendRequestService
    {
        Task<IEnumerable<FriendRequestResponse>> GetAllSentRequestsAsync(int userId);
        Task<IEnumerable<FriendRequestResponse>> GetAllReceivedRequestsAsync(int userId);
        Task<bool> CreateAsync(int senderId, int receiverId);
        Task<bool> DeleteAsync(int senderId, int id);
        Task<bool> FriendRequestExists(int id);
        Task<bool> FriendRequestExists(int senderId, int receiverId);
        Task<bool> AcceptRequest(int receiverId, int id);
        Task<bool> DenyRequest(int receiverId, int id);
    }
}
