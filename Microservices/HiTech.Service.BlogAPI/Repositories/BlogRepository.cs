using HiTech.Service.BlogAPI.Data;
using HiTech.Service.BlogAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.BlogAPI.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _context;
        public BlogRepository(BlogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Entities.Blog blog)
        {
            await _context.AddAsync(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Entities.Blog blog)
        {
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Entities.Blog>> GetAllAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<IEnumerable<Entities.Blog>> GetAllByAuthorIdAsync(int authorId)
        {
            return await _context.Blogs
                .Where(b => b.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<Entities.Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }

        public async Task UpdateAsync(Entities.Blog blog)
        {
            _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
        }
    }
}
