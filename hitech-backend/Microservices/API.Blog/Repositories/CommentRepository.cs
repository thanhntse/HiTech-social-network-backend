using API.Blog.DAL;
using API.Blog.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Blog.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly BlogDbContext _context;
        public CommentRepository(BlogDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllByBlogId(int blogId)
        {
            return await _context.Comments.Where(c => c.BlogId == blogId)
                .ToListAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await _context.Comments.FindAsync(id);
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }
    }
}
