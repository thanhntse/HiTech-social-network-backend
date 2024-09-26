using HiTech.Service.PostsAPI.Entities;
using HiTech.Service.PostsAPI.Services.IService;
using HiTech.Service.PostsAPI.UOW;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.PostsAPI.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<LikeService> _logger;

        public LikeService(ILogger<LikeService> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
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

        public async Task<IEnumerable<int>> GetAllAuthorIDByPostIDAsync(int postId)
        {
            var likes = await _unitOfWork.Likes.FindAllAsync(l => l.PostId == postId);
            return likes.Select(l => l.AuthorId).ToList();
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
