using BlogWebAPI.Entities;

namespace BlogWebAPI.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog> GetByIdAsync(int id);
        Task AddAsync(Blog blog);
        Task UpdateAsync(Blog blog);
        Task DeleteAsync(Blog blog);
    }
}
