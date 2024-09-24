using AutoMapper;
using HiTech.Service.PostsAPI.DTOs.Request;
using HiTech.Service.PostsAPI.DTOs.Response;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Service.PostsAPI.Repositories;
using HiTech.Service.PostsAPI.Services.IService;

namespace HiTech.Service.PostsAPI.Services
{
    public class PostService : IPostService
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ILogger<PostService> _logger;

        public PostService(IPostRepository postRepository, ILogger<PostService> logger, IMapper mapper, IImageRepository imageRepository)
        {
            _postRepository = postRepository;
            _logger = logger;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        public async Task<PostResponse?> CreateAsync(string authorId, PostRequest request)
        {
            var post = _mapper.Map<Post>(request);
            post.AuthorId = Int32.Parse(authorId);

            Post? response;
            try
            {
                response = await _postRepository.CreateAsync(post);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the post at {Time}.", DateTime.Now);
                return null;
            }
            return _mapper.Map<PostResponse>(response);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool result = false;
            var post = await _postRepository.GetByIDAsync(id);

            if (post != null)
            {
                try
                {
                    result = await _postRepository.DeleteAsync(post);
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
            var posts = await _postRepository.GetAllWithImageAsync();
            return _mapper.Map<IEnumerable<PostResponse>>(posts);
        }

        public async Task<PostResponse?> GetByIDWithImageAsync(int id)
        {
            var post = await _postRepository.GetByIDWithImageAsync(id);

            if (post == null)
            {
                return null;
            }

            return _mapper.Map<PostResponse>(post);
        }

        public async Task<bool> PostExists(int id)
        {
            var post = await _postRepository.GetByIDAsync(id);
            return post != null;
        }

        public async Task<bool> UpdateAsync(int id, PostRequest request)
        {
            bool result = false;
            var post = await _postRepository.GetByIDAsync(id);

            if (post != null)
            {
                try
                {
                    await _imageRepository.RemoveAllByPostIDAsync(id);
                    _mapper.Map(request, post);
                    post.UpdateAt = DateTime.Now;

                    result = await _postRepository.UpdateAsync(post);
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
