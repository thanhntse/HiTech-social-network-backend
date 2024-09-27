using AutoMapper;
using HiTech.RabbitMQ.Publisher;
using HiTech.Service.PostsAPI.DTOs.Message;
using HiTech.Service.PostsAPI.DTOs.Response;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Service.PostsAPI.Services.IService;
using HiTech.Service.PostsAPI.UOW;
using HiTech.Shared.Constant;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.PostsAPI.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<LikeService> _logger;
        private readonly IMessagePublisher _messagePublisher;

        public LikeService(ILogger<LikeService> logger, IUnitOfWork unitOfWork, IMapper mapper,
            IMessagePublisher messagePublisher)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messagePublisher = messagePublisher;
        }

        public async Task<bool> CreateAsync(string authorId, int postId)
        {
            var like = new Like()
            {
                AuthorId = Int32.Parse(authorId),
                PostId = postId
            };

            bool result;
            try
            {
                result = await _unitOfWork.SaveWithTransactionAsync(async () =>
                {
                    await _unitOfWork.Likes.CreateAsync(like);

                    // update like in post
                    var post = await _unitOfWork.Posts.GetByIDAsync(postId);
                    // post != null is already check in LikesController
                    if (post != null)
                    {
                        post.Like++;
                        _unitOfWork.Posts.Update(post);
                    }
                }) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the like at {Time}.", DateTime.Now);
                result = false;
            }
            if (result)
            {
                // Add success => send message
                var user = await _unitOfWork.Users.GetByIDAsync(like.AuthorId);
                var post = await _unitOfWork.Posts.GetByIDAsync(like.PostId);
                // Notification
                var notification = new NotificationMessage
                {
                    Type = NotificationType.LIKE_CREATION,
                    Content = user?.FullName + NotificationContent.LIKE_CREATION,
                    UserId = post?.AuthorId
                };
                _messagePublisher.Publish(MessageQueueConstants.NOTI_CREATE_QUEUE, notification);
                _logger.LogInformation("========Create like successfully, notification message sent at {Time}.=========", DateTime.Now);
            }
            return result;
        }

        public async Task<bool> DeleteAsync(string authorId, int postId)
        {
            bool result = false;
            var like = await _unitOfWork.Likes.FindAll(
                    l => l.AuthorId == Int32.Parse(authorId)
                    && l.PostId == postId
                ).FirstOrDefaultAsync();

            if (like != null)
            {
                try
                {
                    result = await _unitOfWork.SaveWithTransactionAsync(async () =>
                    {
                        _unitOfWork.Likes.Delete(like);

                        // update like in post
                        var post = await _unitOfWork.Posts.GetByIDAsync(postId);
                        // post != null is already check in LikesController
                        if (post != null)
                        {
                            post.Like--;
                            _unitOfWork.Posts.Update(post);
                        }
                    }) > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the like at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            return result;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUserByPostIDAsync(int postId)
        {
            var users = await _unitOfWork.Likes.FindAll(l => l.PostId == postId)
                                               .Include(l => l.User)
                                               .Select(l => l.User)
                                               .ToListAsync();
            return _mapper.Map<IEnumerable<UserResponse>>(users);
        }

        public async Task<bool> LikeExists(string authorId, int postId)
        {
            var like = await _unitOfWork.Likes.FindAll(
                    l => l.AuthorId == Int32.Parse(authorId)
                    && l.PostId == postId
                ).FirstOrDefaultAsync();
            return like != null;
        }
    }
}
