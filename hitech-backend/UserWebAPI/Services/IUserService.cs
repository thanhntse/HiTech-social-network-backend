using UserWebAPI.DTO.Request;
using UserWebAPI.DTO.Response;

namespace UserWebAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
        Task<UserResponse> GetUserByIdAsync(int id);
        Task<UserResponse> CreateUserAsync(UserRequest request);
        Task<UserResponse> UpdateUserAsync(int id, UserRequest request);
        Task DeleteUserAsync(int id);
    }
}
