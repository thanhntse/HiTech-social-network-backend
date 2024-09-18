using HiTech.Service.BlogAPI.Data;
using HiTech.Service.BlogAPI.DTOs.Response;
using HiTech.Service.BlogAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.BlogAPI.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogDbContext _context;

        public TagRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<IEnumerable<Tag>> GetAllByBlogIdAsync(int blogId)
        {
            return await _context.BlogTags
                .Where(bt => bt.BlogId == blogId)
                .Include(bt => bt.Tag)
                .Select(bt => new Tag
                {
                    TagId = bt.Tag.TagId,
                    TagName = bt.Tag.TagName
                })
                .ToListAsync();
        }

        public async Task<Tag> GetByIdAsync(int id)
        {
            return await _context.Tags.FindAsync(id);
        }
    }
}
