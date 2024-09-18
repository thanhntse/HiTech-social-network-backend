using HiTech.Service.BlogAPI.Entities;

namespace HiTech.Service.BlogAPI.Repositories
{
    public interface IBlogTagRepository
    {
        Task AddAsync(BlogTag blogTag);
        Task UpdateAsync(BlogTag blogTag);
        Task DeleteAsync(BlogTag blogTag);
    }
}
