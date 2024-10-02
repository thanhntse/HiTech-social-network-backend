using HiTech.Service.GroupAPI.DTOs.Request;
using HiTech.Service.GroupAPI.DTOs.Response;

namespace HiTech.Service.GroupAPI.Services.IService
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupResponse>> GetAllAsync();
        Task<IEnumerable<GroupResponse>> GetAllByUserIDAsync(int userId);
        Task<IEnumerable<UserResponse>> GetAllUserByGroupIDAsync(int groupId);
        Task<GroupResponse?> GetByIDAsync(int groupId);
        Task<GroupResponse?> CreateAsync(int founderId, GroupRequest request);
        Task<bool> UpdateAsync(int groupId, GroupRequest request);
        Task<bool> DeleteAsync(int groupId);
        Task<bool> KickUser(int groupId, int userId);
        Task<bool> OutGroup(int groupId, int userId);
        Task<bool> GroupExists(int groupId);
        Task<bool> UserExistsInGroup(int userId, int groupId);
        Task<bool> IsFounder(int founderId, int groupId);
    }
}
