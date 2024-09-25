using AutoMapper;
using HiTech.Service.PostsAPI.DTOs.Request;
using HiTech.Service.PostsAPI.DTOs.Response;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Service.PostsAPI.Services.IService;
using HiTech.Service.PostsAPI.UOW;
using Microsoft.Extensions.Hosting;

namespace HiTech.Service.PostsAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CommentService> _logger;

        public CommentService(ILogger<CommentService> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

            Comment? response;
            try
            {
                response = await _unitOfWork.Comments.CreateAsync(comment);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the comment at {Time}.", DateTime.Now);
                return null;
            }
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
                    _unitOfWork.Comments.Delete(comment);
                    result = await _unitOfWork.SaveAsync() > 0;
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
            var comments = await _unitOfWork.Comments.FindAllAsync(c => c.PostId == id);
            return _mapper.Map<IEnumerable<CommentReponse>>(comments);
        }

        public async Task<CommentReponse?> GetByIDAsync(int id)
        {
            var comment = await _unitOfWork.Comments.GetByIDAsync(id);

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
