using HiTech.Service.AccountAPI.Data;
using HiTech.Service.AccountAPI.DTOs.Request;
using HiTech.Service.AccountAPI.DTOs.Response;
using HiTech.Service.AccountAPI.Entities;
using HiTech.Service.AccountAPI.Repositories;
using HiTech.Service.AccountAPI.Util;
using AutoMapper;
using System.Data;
using System.Net;
using System.Numerics;

namespace HiTech.Service.AccountAPI.Services
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
            var user = _mapper.Map<Entities.User>(request);
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

        public async Task<bool> DeleteUserAsync(int id)
        {
            bool result = false;
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
                result = true;
            }
            return result;
        }
    }
}
