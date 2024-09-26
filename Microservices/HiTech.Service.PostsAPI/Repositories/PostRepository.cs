using HiTech.Service.PostsAPI.Data;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.PostsAPI.Repositories
{
    public interface IPostRepository : IGenericRepository<Post, int>
    {
        Task<IEnumerable<Post>> GetAllWithImageAsync();
        Task<Post?> GetByIDWithImageAsync(int id);
    }

    public sealed class PostRepository
        : GenericRepository<PostDbContext, Post, int>, IPostRepository
    {
        public PostRepository(PostDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Post>> GetAllWithImageAsync()
            => await _dbSet.Include(p => p.Images)
                           .Include(p => p.User)
                           .ToListAsync();

        public async Task<Post?> GetByIDWithImageAsync(int id)
            => await _dbSet.Include(p => p.Images)
                           .Include(p => p.User)
                           .FirstOrDefaultAsync(i => i.PostId == id);
    }
}
