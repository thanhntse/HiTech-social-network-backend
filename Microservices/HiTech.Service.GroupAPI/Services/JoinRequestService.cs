using AutoMapper;
using HiTech.RabbitMQ.Publisher;
using HiTech.Service.GroupAPI.DTOs.Message;
using HiTech.Service.GroupAPI.DTOs.Response;
using HiTech.Service.GroupAPI.Entities;
using HiTech.Service.GroupAPI.Services.IService;
using HiTech.Service.GroupAPI.UOW;
using HiTech.Shared.Constant;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.GroupAPI.Services
{
    public class JoinRequestService : IJoinRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<JoinRequestService> _logger;
        private readonly IMessagePublisher _messagePublisher;

        public JoinRequestService(ILogger<JoinRequestService> logger, IUnitOfWork unitOfWork, IMapper mapper,
            IMessagePublisher messagePublisher)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messagePublisher = messagePublisher;
        }

        public async Task<bool> AcceptRequest(int founderId, int reqId)
        {
            bool result = false;
            var req = await _unitOfWork.JoinRequests.GetDetailByIDAsync(reqId);
            if (req != null && req.Status == "Pending" && req.Group.FounderId == founderId)
            {
                try
                {
                    result = await _unitOfWork.SaveWithTransactionAsync(async () =>
                    {
                        req.Status = "Accepted";
                        _unitOfWork.JoinRequests.Update(req);

                        var groupUser = new GroupUser()
                        {
                            GroupId = req.GroupId,
                            UserId = req.UserId
                        };
                        await _unitOfWork.GroupUsers.CreateAsync(groupUser);
                    }) > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while accept the join request at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            if (result && req != null)
            {
                // accepted => send message
                // Notification
                var notification = new NotificationMessage
                {
                    Type = NotificationType.ACCEPT_JOIN_GROUP,
                    Content = req.Group.GroupName + NotificationContent.ACCEPT_JOIN_GROUP,
                    UserId = req.UserId
                };
                _messagePublisher.Publish(MessageQueueConstants.NOTI_CREATE_QUEUE, notification);
                _logger.LogInformation("========Accept join request successfully, notification message sent at {Time}.=========", DateTime.Now);
            }
            return result;
        }

        public async Task<bool> CreateAsync(int userId, int groupId)
        {
            var joinReq = new JoinRequest()
            {
                UserId = userId,
                GroupId = groupId
            };

            bool result;
            try
            {
                await _unitOfWork.JoinRequests.CreateAsync(joinReq);
                result = await _unitOfWork.SaveAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the join request at {Time}.", DateTime.Now);
                result = false;
            }
            if (result)
            {
                // Add success => send message
                var user = await _unitOfWork.Users.GetByIDAsync(userId);
                var group = await _unitOfWork.Groups.GetByIDAsync(groupId);
                // Notification
                var notification = new NotificationMessage
                {
                    Type = NotificationType.JOIN_GROUP_REQUEST,
                    Content = user?.FullName + NotificationContent.JOIN_GROUP_REQUEST + group?.GroupName,
                    UserId = group?.FounderId
                };
                _messagePublisher.Publish(MessageQueueConstants.NOTI_CREATE_QUEUE, notification);
                _logger.LogInformation("========Create join request successfully, notification message sent at {Time}.=========", DateTime.Now);
            }
            return result;
        }

        public async Task<bool> DeleteAsync(int userId, int reqId)
        {
            bool result = false;
            var req = await _unitOfWork.JoinRequests.GetByIDAsync(reqId);

            if (req != null && req.UserId == userId)
            {
                try
                {
                    _unitOfWork.JoinRequests.Delete(req);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the join request at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            return result;
        }

        public async Task<bool> DenyRequest(int founderId, int reqId)
        {
            bool result = false;
            var req = await _unitOfWork.JoinRequests.GetDetailByIDAsync(reqId);
            if (req != null && req.Status == "Pending" && req.Group.FounderId == founderId)
            {
                try
                {
                    req.Status = "Denied";
                    _unitOfWork.JoinRequests.Update(req);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deny the join request at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            if (result && req != null)
            {
                // accepted => send message
                // Notification
                var notification = new NotificationMessage
                {
                    Type = NotificationType.DENY_JOIN_GROUP,
                    Content = req.Group.GroupName + NotificationContent.DENY_JOIN_GROUP,
                    UserId = req.UserId
                };
                _messagePublisher.Publish(MessageQueueConstants.NOTI_CREATE_QUEUE, notification);
                _logger.LogInformation("========Deny join request successfully, notification message sent at {Time}.=========", DateTime.Now);
            }
            return result;
        }

        public async Task<IEnumerable<JoinRequestResponse>> GetAllByGroupIDAsync(int groupId)
        {
            var reqs = await _unitOfWork.JoinRequests.FindAll(i => i.GroupId == groupId)
                                                     .Include(i => i.User)
                                                     .ToListAsync();
            return _mapper.Map<IEnumerable<JoinRequestResponse>>(reqs);
        }

        public async Task<IEnumerable<JoinRequestResponse>> GetAllPendingRequestByGroupIDAsync(int groupId)
        {
            var reqs = await _unitOfWork.JoinRequests.FindAll(i => i.GroupId == groupId
                                                           && i.Status == "Pending")
                                                     .Include(i => i.User)
                                                     .ToListAsync();
            return _mapper.Map<IEnumerable<JoinRequestResponse>>(reqs);
        }

        public async Task<bool> JoinRequestExists(int reqId)
        {
            return await _unitOfWork.JoinRequests.AnyAsync(fr => fr.JoinRequestId == reqId);
        }

        public async Task<bool> JoinRequestExists(int userId, int groupId)
        {
            return await _unitOfWork.JoinRequests.AnyAsync(
                    fr => fr.UserId == userId && fr.GroupId == groupId && fr.Status == "Pending"
                );
        }
    }
}
