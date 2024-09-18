using HiTech.Service.BlogAPI.Entities;

namespace HiTech.Service.BlogAPI.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<IEnumerable<Tag>> GetAllByBlogIdAsync(int blogId);
        Task<Tag> GetByIdAsync(int id);
    }
}
