using HiTech.Service.PostsAPI.Data;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.PostsAPI.Repositories
{
    public interface IImageRepository : IGenericRepository<Image, int>
    {
        Task<bool> RemoveAllByPostIDAsync(int postId);
    }

    public sealed class ImageRepository
        : GenericRepository<PostDbContext, Image, int>, IImageRepository
    {
        public ImageRepository(PostDbContext context) : base(context)
        {
        }

        public async Task<bool> RemoveAllByPostIDAsync(int postId)
        {
            var images = await DbSet.Where(i => i.PostId == postId).ToListAsync();
            DbSet.RemoveRange(images.ToArray());
            return await _context.SaveChangesAsync(CancellationToken.None) > 0;
        }
    }
}
