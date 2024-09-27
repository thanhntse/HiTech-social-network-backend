using AutoMapper;
using HiTech.RabbitMQ.Publisher;
using HiTech.Service.PostsAPI.DTOs.Message;
using HiTech.Service.PostsAPI.DTOs.Request;
using HiTech.Service.PostsAPI.DTOs.Response;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Service.PostsAPI.Services.IService;
using HiTech.Service.PostsAPI.UOW;
using HiTech.Shared.Constant;

namespace HiTech.Service.PostsAPI.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostService> _logger;
        private readonly IMessagePublisher _messagePublisher;

        public PostService(ILogger<PostService> logger, IMapper mapper, IUnitOfWork unitOfWork,
            IMessagePublisher messagePublisher)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _messagePublisher = messagePublisher;
        }

        public async Task<PostResponse?> CreateAsync(string authorId, PostRequest request)
        {
            var post = _mapper.Map<Post>(request);
            post.AuthorId = Int32.Parse(authorId);

            Post? response;
            try
            {
                response = await _unitOfWork.Posts.CreateAsync(post);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the post at {Time}.", DateTime.Now);
                return null;
            }
            // Add success => send message
            // Notification
            var notification = new NotificationMessage
            {
                Type = NotificationType.POST_CREATION,
                Content = NotificationContent.POST_CREATION,
                UserId = response.AuthorId
            };
            _messagePublisher.Publish(MessageQueueConstants.NOTI_CREATE_QUEUE, notification);
            _logger.LogInformation("========Create post successfully, notification message sent at {Time}.=========", DateTime.Now);

            // Return to controller
            return _mapper.Map<PostResponse>(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            var post = await _unitOfWork.Posts.GetByIDAsync(id);

            if (post != null)
            {
                try
                {
                    _unitOfWork.Posts.Delete(post);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the post at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            return result;
        }

        public async Task<IEnumerable<PostResponse>> GetAllWithImageAsync()
        {
            var posts = await _unitOfWork.Posts.GetAllWithImageAsync();
            return _mapper.Map<IEnumerable<PostResponse>>(posts);
        }

        public async Task<PostResponse?> GetByIDWithImageAsync(int id)
        {
            var post = await _unitOfWork.Posts.GetByIDWithImageAsync(id);

            if (post == null)
            {
                return null;
            }

            return _mapper.Map<PostResponse>(post);
        }

        public async Task<bool> PostExists(int id)
        {
            var post = await _unitOfWork.Posts.GetByIDAsync(id);
            return post != null;
        }

        public async Task<bool> UpdateAsync(int id, PostRequest request)
        {
            bool result = false;
            var post = await _unitOfWork.Posts.GetByIDAsync(id);

            if (post != null)
            {
                try
                {
                    result = await _unitOfWork.SaveWithTransactionAsync(async () =>
                    {
                        var images = await _unitOfWork.Images.FindAllAsync(i => i.PostId == id);
                        _unitOfWork.Images.DeleteRange(images);

                        _mapper.Map(request, post);
                        post.UpdateAt = DateTime.Now;
                        _unitOfWork.Posts.Update(post);
                    }) > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the post at {Time}.", DateTime.Now);
                    result = false;
                }
            }

            return result;
        }
    }
}
