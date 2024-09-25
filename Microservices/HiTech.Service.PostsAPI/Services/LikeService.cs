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
                await _unitOfWork.Likes.CreateAsync(like);
                result = await _unitOfWork.SaveAsync() > 0;
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
                    _unitOfWork.Likes.Delete(like);
                    result = await _unitOfWork.SaveAsync() > 0;
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
    }
}
