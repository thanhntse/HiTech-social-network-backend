using API.Blog.DTO.Request;
using API.Blog.DTO.Response;
using API.Blog.Entities;
using API.Blog.Repositories;
using AutoMapper;

namespace API.Blog.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogTagRepository _blogTagRepository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IBlogTagRepository blogTagRepository, IMapper mapper)
        {
            _blogRepository = blogRepository;
            _mapper = mapper;
            _blogTagRepository = blogTagRepository;
        }

        public async Task<BlogResponse> CreateBlogAsync(BlogRequest request)
        {
            var blog = _mapper.Map<Entities.Blog>(request);
            await _blogRepository.AddAsync(blog);

            request.TagIds.ForEach(
                    item =>
                    {
                        var blogTag = new BlogTag
                        {
                            BlogId = blog.BlogId,
                            TagId = item
                        };
                        _blogTagRepository.AddAsync(blogTag);
                    }
                );
            var blogResponse = _mapper.Map<BlogResponse>(blog);
            var tagResponses = new List<TagResponse>();

            blogResponse.Tags = tagResponses;

            return blogResponse;
        }

        public async Task<bool> DeleteBlogAsync(int id)
        {
            bool result = false;
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog != null)
            {
                await _blogRepository.DeleteAsync(blog);
                result = true;
            }
            return result;
        }

        public async Task<IEnumerable<BlogResponse>> GetAllBlogsAsync()
        {
            var blogs = await _blogRepository.GetAllAsync();
            var blogResponses = _mapper.Map<IEnumerable<BlogResponse>>(blogs);
            return blogResponses;
        }

        public async Task<BlogResponse> GetBlogByIdAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                return null;
            }
            var blogResponse = _mapper.Map<BlogResponse>(blog);
            return blogResponse;
        }

        public async Task<BlogResponse> UpdateBlogAsync(int id, BlogRequest request)
        {
            var blog = await _blogRepository.GetByIdAsync(id);
            if (blog == null)
            {
                return null;
            }
            _mapper.Map(request, blog);
            await _blogRepository.UpdateAsync(blog);
            var blogResponse = _mapper.Map<BlogResponse>(blog);
            return blogResponse;
        }
    }
}
