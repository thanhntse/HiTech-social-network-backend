using API.Blog.Entities;
using API.Blog.Repositories;

namespace API.Blog.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            var tags = await _tagRepository.GetAllAsync();
            return tags;
        }

        public async Task<Tag> GetTagByIdAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null)
            {
                return null;
            }
            return tag;
        }
    }
}
