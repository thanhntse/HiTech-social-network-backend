using AutoMapper;
using HiTech.RabbitMQ.Publisher;
using HiTech.Service.FriendAPI.DTOs.Message;
using HiTech.Service.FriendAPI.DTOs.Response;
using HiTech.Service.FriendAPI.Entities;
using HiTech.Service.FriendAPI.Services.IService;
using HiTech.Service.FriendAPI.UOW;
using HiTech.Shared.Constant;

namespace HiTech.Service.FriendAPI.Services
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FriendRequestService> _logger;
        private readonly IMessagePublisher _messagePublisher;

        public FriendRequestService(ILogger<FriendRequestService> logger, IUnitOfWork unitOfWork, IMapper mapper,
            IMessagePublisher messagePublisher)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messagePublisher = messagePublisher;
        }

        public async Task<bool> AcceptRequest(int receiverId, int id)
        {
            bool result = false;
            var req = await _unitOfWork.FriendRequests.GetByIDAsync(id);
            if (req != null && req.Status == "Pending" && req.ReceiverId == receiverId)
            {
                try
                {
                    result = await _unitOfWork.SaveWithTransactionAsync(async () =>
                    {
                        req.Status = "Accepted";
                        _unitOfWork.FriendRequests.Update(req);

                        var fship = new Friendship()
                        {
                            UserSentId = req.SenderId,
                            UserReceivedId = req.ReceiverId
                        };
                        await _unitOfWork.Friendships.CreateAsync(fship);
                    }) > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while accept the friend request at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            if (result && req != null)
            {
                // accepted => send message
                var receiver = await _unitOfWork.Users.GetByIDAsync(req.ReceiverId);
                // Notification
                var notification = new NotificationMessage
                {
                    Type = NotificationType.FRIEND_REQUEST_ACCEPT,
                    Content = receiver?.FullName + NotificationContent.FRIEND_REQUEST_ACCEPT,
                    UserId = req?.SenderId
                };
                _messagePublisher.Publish(MessageQueueConstants.NOTI_CREATE_QUEUE, notification);
                _logger.LogInformation("========Accept friend request successfully, notification message sent at {Time}.=========", DateTime.Now);
            }
            return result;
        }

        public async Task<bool> CreateAsync(int senderId, int receiverId)
        {
            var friendRequest = new FriendRequest()
            {
                SenderId = senderId,
                ReceiverId = receiverId
            };

            bool result;
            try
            {
                await _unitOfWork.FriendRequests.CreateAsync(friendRequest);
                result = await _unitOfWork.SaveAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the friend request at {Time}.", DateTime.Now);
                result = false;
            }
            if (result)
            {
                // Add success => send message
                var sender = await _unitOfWork.Users.GetByIDAsync(senderId);
                // Notification
                var notification = new NotificationMessage
                {
                    Type = NotificationType.FRIEND_REQUEST_CREATION,
                    Content = sender?.FullName + NotificationContent.FRIEND_REQUEST_CREATION,
                    UserId = receiverId
                };
                _messagePublisher.Publish(MessageQueueConstants.NOTI_CREATE_QUEUE, notification);
                _logger.LogInformation("========Create friend request successfully, notification message sent at {Time}.=========", DateTime.Now);
            }
            return result;
        }

        public async Task<bool> DeleteAsync(int senderId, int id)
        {
            bool result = false;
            var req = await _unitOfWork.FriendRequests.GetByIDAsync(id);

            if (req != null && req.SenderId == senderId)
            {
                try
                {
                    _unitOfWork.FriendRequests.Delete(req);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the friend request at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            return result;
        }

        public async Task<bool> DenyRequest(int receiverId, int id)
        {
            bool result = false;
            var req = await _unitOfWork.FriendRequests.GetByIDAsync(id);
            if (req != null && req.Status == "Pending" && req.ReceiverId == receiverId)
            {
                try
                {
                    req.Status = "Denied";
                    _unitOfWork.FriendRequests.Update(req);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while denied the friend request at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            if (result && req != null)
            {
                // denied => send message
                var receiver = await _unitOfWork.Users.GetByIDAsync(req.ReceiverId);
                // Notification
                var notification = new NotificationMessage
                {
                    Type = NotificationType.FRIEND_REQUEST_DENIED,
                    Content = receiver?.FullName + NotificationContent.FRIEND_REQUEST_DENIED,
                    UserId = req?.SenderId
                };
                _messagePublisher.Publish(MessageQueueConstants.NOTI_CREATE_QUEUE, notification);
                _logger.LogInformation("========Denied friend request successfully, notification message sent at {Time}.=========", DateTime.Now);
            }
            return result;
        }

        public async Task<bool> FriendRequestExists(int senderId, int receiverId)
        {
            return await _unitOfWork.FriendRequests.AnyAsync(
                    fr => (fr.SenderId == senderId && fr.ReceiverId == receiverId)
                   || (fr.SenderId == receiverId && fr.ReceiverId == senderId)
                );
        }

        public async Task<bool> FriendRequestExists(int id)
        {
            return await _unitOfWork.FriendRequests.AnyAsync(fr => fr.FriendRequestId == id);
        }

        public async Task<IEnumerable<FriendRequestResponse>> GetAllReceivedRequestsAsync(int userId)
        {
            var reqs = await _unitOfWork.FriendRequests.GetAllReceivedRequestsAsync(userId);
            return _mapper.Map<IEnumerable<FriendRequestResponse>>(reqs);
        }

        public async Task<IEnumerable<FriendRequestResponse>> GetAllSentRequestsAsync(int userId)
        {
            var reqs = await _unitOfWork.FriendRequests.GetAllSentRequestsAsync(userId);
            return _mapper.Map<IEnumerable<FriendRequestResponse>>(reqs);
        }
    }
}
