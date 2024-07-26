using API.Blog.Entities;

namespace API.Blog.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<IEnumerable<Tag>> GetAllByBlogIdAsync(int blogId);
        Task<Tag> GetByIdAsync(int id);
    }
}
