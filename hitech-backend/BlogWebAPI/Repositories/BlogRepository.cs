using BlogWebAPI.DAL;
using BlogWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogWebAPI.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _context;
        public BlogRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Blog blog)
        {
            await _context.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Blog blog)
        {
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }

        public async Task UpdateAsync(Blog blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }
    }
}
