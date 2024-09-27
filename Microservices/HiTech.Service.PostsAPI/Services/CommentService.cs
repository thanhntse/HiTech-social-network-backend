using AutoMapper;
using HiTech.RabbitMQ.Publisher;
using HiTech.Service.PostsAPI.DTOs.Message;
using HiTech.Service.PostsAPI.DTOs.Request;
using HiTech.Service.PostsAPI.DTOs.Response;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Service.PostsAPI.Services.IService;
using HiTech.Service.PostsAPI.UOW;
using HiTech.Shared.Constant;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.PostsAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CommentService> _logger;
        private readonly IMessagePublisher _messagePublisher;

        public CommentService(ILogger<CommentService> logger, IMapper mapper, IUnitOfWork unitOfWork,
            IMessagePublisher messagePublisher)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _messagePublisher = messagePublisher;
        }

        public async Task<bool> CommentExists(int id)
        {
            var comment = await _unitOfWork.Comments.GetByIDAsync(id);
            return comment != null;
        }

        public async Task<CommentReponse?> CreateAsync(string authorId, CommentRequest request)
        {
            var comment = _mapper.Map<Comment>(request);
            comment.AuthorId = Int32.Parse(authorId);

            Comment? response = new();
            try
            {
                await _unitOfWork.SaveWithTransactionAsync(async () =>
                {
                    response = await _unitOfWork.Comments.CreateAsync(comment);

                    // update comment count in post
                    var post = await _unitOfWork.Posts.GetByIDAsync(comment.PostId);
                    // post != null already check in CommentsController
                    if (post != null)
                    {
                        post.CommentsCount++;
                        _unitOfWork.Posts.Update(post);
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the comment at {Time}.", DateTime.Now);
                return null;
            }
            // Add success => send message
            var user = await _unitOfWork.Users.GetByIDAsync(comment.AuthorId);
            var post = await _unitOfWork.Posts.GetByIDAsync(comment.PostId);
            if (user?.UserId != post?.AuthorId)
            {
                // Notification
                var notification = new NotificationMessage
                {
                    Type = NotificationType.COMMENT_CREATION,
                    Content = user?.FullName + NotificationContent.COMMENT_CREATION,
                    UserId = post?.AuthorId
                };
                _messagePublisher.Publish(MessageQueueConstants.NOTI_CREATE_QUEUE, notification);
                _logger.LogInformation("========Create comment successfully, notification message sent at {Time}.=========", DateTime.Now);
            }
            else
            {
                _logger.LogInformation("========Create comment successfully, comment yourself at {Time}.=========", DateTime.Now);
            }
            // Return to controller
            return _mapper.Map<CommentReponse>(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            var comment = await _unitOfWork.Comments.GetByIDAsync(id);

            if (comment != null)
            {
                try
                {
                    result = await _unitOfWork.SaveWithTransactionAsync(async () =>
                    {
                        _unitOfWork.Comments.Delete(comment);

                        // update comment count in post
                        var post = await _unitOfWork.Posts.GetByIDAsync(comment.PostId);
                        // post != null already check in CommentsController
                        if (post != null)
                        {
                            post.CommentsCount--;
                            _unitOfWork.Posts.Update(post);
                        }
                    }) > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while deleting the comment at {Time}.", DateTime.Now);
                    result = false;
                }
            }
            return result;
        }

        public async Task<IEnumerable<CommentReponse>> GetAllByPostIDAsync(int id)
        {
            var comments = await _unitOfWork.Comments.FindAll(c => c.PostId == id)
                                                     .Include(c => c.User)
                                                     .ToListAsync();
            return _mapper.Map<IEnumerable<CommentReponse>>(comments);
        }

        public async Task<CommentReponse?> GetByIDAsync(int id)
        {
            var comment = await _unitOfWork.Comments.FindAll(c => c.CommentId == id)
                                                    .Include(c => c.User)
                                                    .FirstOrDefaultAsync();

            if (comment == null)
            {
                return null;
            }

            return _mapper.Map<CommentReponse>(comment);
        }

        public async Task<bool> UpdateAsync(int id, CommentRequest request)
        {
            bool result = false;
            var commnet = await _unitOfWork.Comments.GetByIDAsync(id);

            if (commnet != null)
            {
                try
                {
                    _mapper.Map(request, commnet);
                    commnet.UpdateAt = DateTime.Now;
                    _unitOfWork.Comments.Update(commnet);
                    result = await _unitOfWork.SaveAsync() > 0;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while updating the comment at {Time}.", DateTime.Now);
                    result = false;
                }
            }

            return result;
        }
    }
}
