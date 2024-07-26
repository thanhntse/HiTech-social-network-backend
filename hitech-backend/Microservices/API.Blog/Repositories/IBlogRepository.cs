using API.Blog.Entities;

namespace API.Blog.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Entities.Blog>> GetAllAsync();
        Task<IEnumerable<Entities.Blog>> GetAllByAuthorIdAsync(int authorId);
        Task<Entities.Blog> GetByIdAsync(int id);
        Task AddAsync(Entities.Blog blog);
        Task UpdateAsync(Entities.Blog blog);
        Task DeleteAsync(Entities.Blog blog);
    }
}
