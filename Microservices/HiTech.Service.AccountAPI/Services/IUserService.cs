using HiTech.Service.AccountAPI.DTOs.Request;
using HiTech.Service.AccountAPI.DTOs.Response;

namespace HiTech.Service.AccountAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<UserResponse> GetUserByIdAsync(int id);
        Task<UserResponse> CreateUserAsync(UserRequest request);
        Task<UserResponse> UpdateUserAsync(int id, UserRequest request);
        Task<bool> DeleteUserAsync(int id);
    }
}
