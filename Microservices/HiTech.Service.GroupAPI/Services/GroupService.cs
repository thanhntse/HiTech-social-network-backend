using AutoMapper;
using HiTech.RabbitMQ.Publisher;
using HiTech.Service.GroupAPI.DTOs.Message;
using HiTech.Service.GroupAPI.DTOs.Request;
using HiTech.Service.GroupAPI.DTOs.Response;
using HiTech.Service.GroupAPI.Entities;
using HiTech.Service.GroupAPI.Services.IService;
using HiTech.Service.GroupAPI.UOW;
using HiTech.Shared.Constant;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.GroupAPI.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<GroupService> _logger;
        private readonly IMessagePublisher _messagePublisher;

        public GroupService(ILogger<GroupService> logger, IUnitOfWork unitOfWork, IMapper mapper,
            IMessagePublisher messagePublisher)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messagePublisher = messagePublisher;
        }

        public async Task<GroupResponse?> CreateAsync(int founderId, GroupRequest request)
        {
            var group = _mapper.Map<Group>(request);
            group.FounderId = founderId;

            try
            {
                var result = await _unitOfWork.Groups.CreateAsync(group);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<GroupResponse>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the group at {Time}.", DateTime.Now);
            }
            return null;
        }

        public async Task<bool> DeleteAsync(int groupId)
        {
            bool result = false;
            var group = await _unitOfWork.Groups.GetByIDAsync(groupId);
            if (group != null)
            {
                try
                {
                    _unitOfWork.Groups.Delete(group);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the group at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            return result;
        }

        public async Task<IEnumerable<GroupResponse>> GetAllAsync()
        {
            var groups = await _unitOfWork.Groups.GetAllAsync();
            return _mapper.Map<IEnumerable<GroupResponse>>(groups);
        }

        public async Task<IEnumerable<GroupResponse>> GetAllByUserIDAsync(int userId)
        {
            var groups = await _unitOfWork.Users.GetAllGroupByUserIDAsync(userId);
            return _mapper.Map<IEnumerable<GroupResponse>>(groups);
        }

        public async Task<GroupResponse?> GetByIDAsync(int groupId)
        {
            var group = await _unitOfWork.Groups.GetDetailByIDAsync(groupId);
            if (group == null)
            {
                return null;
            }
            return _mapper.Map<GroupResponse>(group);
        }

        public async Task<bool> GroupExists(int groupId)
        {
            return await _unitOfWork.Groups.AnyAsync(g => g.GroupId == groupId);
        }

        public async Task<bool> IsFounder(int founderId, int groupId)
        {
            var group = await _unitOfWork.Groups.GetByIDAsync(groupId);
            return group?.FounderId == founderId;
        }

        public async Task<bool> KickUser(int groupId, int userId)
        {
            bool result = false;
            var groupUser = await _unitOfWork.GroupUsers.FindAll(
                           gu => gu.GroupId == groupId
                           && gu.UserId == userId
                       ).FirstOrDefaultAsync();

            if (groupUser != null)
            {
                try
                {
                    _unitOfWork.GroupUsers.Delete(groupUser);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while kick a user at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            if (result)
            {
                // Kick success => send message
                var group = await _unitOfWork.Groups.GetByIDAsync(groupId);
                // Notification
                var notification = new NotificationMessage
                {
                    Type = NotificationType.BEING_KICKED,
                    Content = NotificationContent.BEING_KICKED + group?.GroupName,
                    UserId = userId
                };
                _messagePublisher.Publish(MessageQueueConstants.NOTI_CREATE_QUEUE, notification);
                _logger.LogInformation("========Kick user successfully, notification message sent at {Time}.=========", DateTime.Now);
            }
            return result;
        }

        public async Task<bool> UpdateAsync(int groupId, GroupRequest request)
        {
            bool result = false;
            var group = await _unitOfWork.Groups.GetByIDAsync(groupId);

            if (group != null)
            {
                try
                {
                    _mapper.Map(request, group);
                    _unitOfWork.Groups.Update(group);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the group at {Time}.", DateTime.Now);
                    result = false;
                }
            }

            return result;
        }

        public async Task<bool> UserExistsInGroup(int userId, int groupId)
        {
            return await _unitOfWork.GroupUsers.AnyAsync(
                     gu => gu.GroupId == groupId
                           && gu.UserId == userId
                );
        }
    }
}
