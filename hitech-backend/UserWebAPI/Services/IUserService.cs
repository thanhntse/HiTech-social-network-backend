using UserWebAPI.DTO.Request;
using UserWebAPI.Entities;

namespace UserWebAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(UserRequest request);
        Task<User> UpdateUserAsync(int id, UserRequest request);
        Task DeleteUserAsync(int id);
    }
}
