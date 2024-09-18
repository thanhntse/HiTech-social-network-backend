using HiTech.Service.BlogAPI.DTOs.Request;
using HiTech.Service.BlogAPI.DTOs.Response;
using HiTech.Service.BlogAPI.Entities;
using HiTech.Service.BlogAPI.Repositories;
using AutoMapper;

namespace HiTech.Service.BlogAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IBlogRepository blogRepository)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _blogRepository = blogRepository;
        }

        public async Task<CommentResponse> CreateCommentAsync(CommentRequest request)
        {
            var comment = _mapper.Map<Comment>(request);
            await _commentRepository.AddAsync(comment);

            var blog = await _blogRepository.GetByIdAsync(comment.BlogId);
            blog.CommentsCount = blog.CommentsCount + 1;
            await _blogRepository.UpdateAsync(blog);

            var commentResponse = _mapper.Map<CommentResponse>(comment);
            return commentResponse;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            bool result = false;
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment != null)
            {
                await _commentRepository.DeleteAsync(comment);
                var blog = await _blogRepository.GetByIdAsync(comment.BlogId);
                blog.CommentsCount = blog.CommentsCount - 1;
                await _blogRepository.UpdateAsync(blog);
                result = true;
            }
            return result;
        }

        public async Task<IEnumerable<CommentResponse>> GetAllCommentsByBlogId(int blogId)
        {
            var comments = await _commentRepository.GetAllByBlogId(blogId);
            var commentResponses = _mapper.Map<IEnumerable<CommentResponse>>(comments);

            return commentResponses;
        }

        public async Task<CommentResponse> GetCommentByIdAsync(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return null;
            }
            var commentResponse = _mapper.Map<CommentResponse>(comment);
            return commentResponse;
        }

        public async Task<CommentResponse> UpdateCommentAsync(int id, CommentRequest request)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return null;
            }
            _mapper.Map(comment, request);
            comment.UpdateAt = DateTime.Now;
            await _commentRepository.UpdateAsync(comment);
            var commentResponse = _mapper.Map<CommentResponse>(comment);
            return commentResponse;
        }
    }
}
