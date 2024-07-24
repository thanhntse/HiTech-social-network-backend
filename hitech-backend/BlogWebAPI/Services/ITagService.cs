using BlogWebAPI.Entities;

namespace BlogWebAPI.Services
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> GetAllTagsAsync();
        Task<Tag> GetTagByIdAsync(int id);
    }
}
