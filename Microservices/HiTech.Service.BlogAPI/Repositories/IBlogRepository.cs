using HiTech.Service.BlogAPI.Entities;

namespace HiTech.Service.BlogAPI.Repositories
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
