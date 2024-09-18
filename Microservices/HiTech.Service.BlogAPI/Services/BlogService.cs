using HiTech.Service.BlogAPI.DTOs.Request;
using HiTech.Service.BlogAPI.DTOs.Response;
using HiTech.Service.BlogAPI.Entities;
using HiTech.Service.BlogAPI.Repositories;
using AutoMapper;
using System.Reflection.Metadata;

namespace HiTech.Service.BlogAPI.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IBlogTagRepository _blogTagRepository;
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;

        public BlogService(IBlogRepository blogRepository, IBlogTagRepository blogTagRepository,
            ITagRepository tagRepository, IMapper mapper)
        {
            _mapper = mapper;
            _blogRepository = blogRepository;
            _blogTagRepository = blogTagRepository;
            _tagRepository = tagRepository;
        }

        private ICollection<TagResponse> GetTagResponseList(int BlogId)
        {
            var tags = _tagRepository.GetAllByBlogIdAsync(BlogId);
            var tagResponses = _mapper.Map<ICollection<TagResponse>>(tags);

            return tagResponses;
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
            blogResponse.Tags = GetTagResponseList(blog.BlogId);

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

            foreach (var item in blogResponses)
            {
                item.Tags = GetTagResponseList(item.BlogId);
            }

            return blogResponses;
        }

        public async Task<IEnumerable<BlogResponse>> GetAllBlogsByAuthorIdAsync(int authorId)
        {
            var blogs = await _blogRepository.GetAllByAuthorIdAsync(authorId);
            var blogResponses = _mapper.Map<IEnumerable<BlogResponse>>(blogs);

            foreach (var item in blogResponses)
            {
                item.Tags = GetTagResponseList(item.BlogId);
            }

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
            blogResponse.Tags = GetTagResponseList(id);
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
            blog.UpdateAt = DateTime.UtcNow;
            await _blogRepository.UpdateAsync(blog);
            var blogResponse = _mapper.Map<BlogResponse>(blog);
            blogResponse.Tags = GetTagResponseList(id);

            return blogResponse;
        }
    }
}
