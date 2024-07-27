using API.Blog.DAL;
using API.Blog.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Blog.Repositories
{
    public class BlogTagRepository : IBlogTagRepository
    {
        private readonly BlogDbContext _context;
        public BlogTagRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BlogTag blogTag)
        {
            await _context.BlogTags.AddAsync(blogTag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(BlogTag blogTag)
        {
            _context.BlogTags.Remove(blogTag);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogTag blogTag)
        {
            _context.BlogTags.Update(blogTag);
            await _context.SaveChangesAsync();
        }
    }
}
