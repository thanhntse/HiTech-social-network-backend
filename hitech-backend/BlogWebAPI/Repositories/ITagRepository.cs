using BlogWebAPI.Entities;

namespace BlogWebAPI.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<Tag> GetByIdAsync(int id);
    }
}
