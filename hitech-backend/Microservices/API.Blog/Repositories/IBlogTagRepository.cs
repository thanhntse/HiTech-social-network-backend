using API.Blog.Entities;

namespace API.Blog.Repositories
{
    public interface IBlogTagRepository
    {
        Task AddAsync(BlogTag blogTag);
        Task UpdateAsync(BlogTag blogTag);
        Task DeleteAsync(BlogTag blogTag);
    }
}
