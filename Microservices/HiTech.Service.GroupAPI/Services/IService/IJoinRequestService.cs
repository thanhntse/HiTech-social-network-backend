using HiTech.Service.GroupAPI.DTOs.Response;

namespace HiTech.Service.GroupAPI.Services.IService
{
    public interface IJoinRequestService
    {
        Task<IEnumerable<JoinRequestResponse>> GetAllByGroupIDAsync(int groupId);
        Task<bool> CreateAsync(int userId, int groupId);
        Task<bool> DeleteAsync(int userId, int reqId);
        Task<bool> JoinRequestExists(int reqId);
        Task<bool> JoinRequestExists(int userId, int groupId);
        Task<bool> AcceptRequest(int founderId, int reqId);
        Task<bool> DenyRequest(int founderId, int reqId);
    }
}
