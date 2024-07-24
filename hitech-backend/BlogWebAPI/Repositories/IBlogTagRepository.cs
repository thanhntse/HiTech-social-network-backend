using BlogWebAPI.Entities;

namespace BlogWebAPI.Repositories
{
    public interface IBlogTagRepository
    {
        Task<IEnumerable<BlogTag>> GetAllByBlogIdAsync(int blogId);
        Task<IEnumerable<BlogTag>> GetAllByTagIdAsync(int tagId);
        Task AddAsync(BlogTag blogTag);
        Task UpdateAsync(BlogTag blogTag);
        Task DeleteAsync(BlogTag blogTag);
    }
}
