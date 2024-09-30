using AutoMapper;
using HiTech.Service.FriendAPI.DTOs.Response;
using HiTech.Service.FriendAPI.Services.IService;
using HiTech.Service.FriendAPI.UOW;

namespace HiTech.Service.FriendAPI.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FriendshipService> _logger;

        public FriendshipService(ILogger<FriendshipService> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> DeleteAsync(int userId, int id)
        {
            bool result = false;
            var fship = await _unitOfWork.Friendships.GetByIDAsync(id);
            bool isUserValid = fship?.UserReceivedId == userId || fship?.UserSentId == userId;

            if (fship != null && isUserValid)
            {
                try
                {
                    _unitOfWork.Friendships.Delete(fship);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the friendship at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            return result;
        }

        public async Task<bool> FriendshipExists(int id)
        {
            return await _unitOfWork.Friendships.AnyAsync(fr => fr.FriendshipId == id);
        }

        public async Task<bool> FriendshipExists(int userId1, int userId2)
        {
            return await _unitOfWork.Friendships.AnyAsync(
            fr => (fr.UserSentId == userId1 && fr.UserReceivedId == userId2)
            || (fr.UserSentId == userId2 && fr.UserReceivedId == userId1)
            );
        }

        public async Task<IEnumerable<UserResponse>> GetAllFriendAsync(int userId)
        {
            var friends = await _unitOfWork.Friendships.GetAllFriendsAsync(userId);
            return _mapper.Map<IEnumerable<UserResponse>>(friends);
        }

        public async Task<IEnumerable<FriendshipResponse>> GetAllFriendDetailAsync(int userId)
        {
            var friendships = await _unitOfWork.Friendships.GetAllFriendsDetailAsync(userId);
            return _mapper.Map<IEnumerable<FriendshipResponse>>(friendships);
        }

        public async Task<bool> ToggleBlockAsync(int userId, int id)
        {
            bool result = false;
            var fship = await _unitOfWork.Friendships.GetByIDAsync(id);
            bool isUserValid = fship?.UserReceivedId == userId || fship?.UserSentId == userId;

            if (fship != null && isUserValid)
            {
                try
                {
                    fship.Status = !fship.Status;
                    _unitOfWork.Friendships.Update(fship);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while toggle block the friendship at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            return result;
        }
    }
}
