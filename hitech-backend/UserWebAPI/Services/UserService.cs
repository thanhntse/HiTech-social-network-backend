using AutoMapper;
using System.Data;
using System.Net;
using System.Numerics;
using UserWebAPI.DAL;
using UserWebAPI.DTO.Request;
using UserWebAPI.DTO.Response;
using UserWebAPI.Entities;
using UserWebAPI.Repositories;
using UserWebAPI.Util;

namespace UserWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var userResponses = _mapper.Map<IEnumerable<UserResponse>>(users);
            return userResponses;
        }

        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            var userResponse = _mapper.Map<UserResponse>(user);

            return userResponse;
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest request)
        {
            var user = _mapper.Map<User>(request);
            user.CreatedDate = DateTime.Now;
            user.Role = "Member";
            user.Password = PasswordEncoder.Encode(request.Password);

            await _userRepository.AddAsync(user);

            var userResponse = _mapper.Map<UserResponse>(user);

            return userResponse;
        }

        public async Task<UserResponse> UpdateUserAsync(int id, UserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            _mapper.Map(request, user);
            user.Password = PasswordEncoder.Encode(request.Password);

            await _userRepository.UpdateAsync(user);

            var userResponse = _mapper.Map<UserResponse>(user);

            return userResponse;
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
