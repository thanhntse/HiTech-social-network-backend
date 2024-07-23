using System.Data;
using System.Net;
using System.Numerics;
using UserWebAPI.DAL;
using UserWebAPI.DTO.Request;
using UserWebAPI.Entities;
using UserWebAPI.Repositories;

namespace UserWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<User> CreateUserAsync(UserRequest request)
        {
            var user = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                CreatedDate = DateTime.UtcNow,
                Password = request.Password,
                Phone = request.Phone,
                Role = "Member",
            };

            await _userRepository.AddAsync(user);

            return user;
        }

        public async Task<User> UpdateUserAsync(int id, UserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.Email = request.Email;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Address = request.Address;
                user.Password = request.Password;
                user.Phone = request.Phone;

                await _userRepository.UpdateAsync(user);
            }
            return user ?? new User();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
            }
        }
    }
}
