using HiTech.Service.PostsAPI.Data;
using HiTech.Service.PostsAPI.Repositories;

namespace HiTech.Service.PostsAPI.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;

        private readonly PostDbContext _context;
        public IPostRepository Posts { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public ILikeRepository Likes { get; private set; }
        public IImageRepository Images { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(PostDbContext context)
        {
            _context = context;
            Posts = new PostRepository(context);
            Comments = new CommentRepository(context);
            Likes = new LikeRepository(context);
            Images = new ImageRepository(context);
            Users = new UserRepository(context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<int> SaveWithTransactionAsync()
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var result = await _context.SaveChangesAsync(CancellationToken.None);
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
        public async Task<int> SaveWithTransactionAsync(Func<Task> operation)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await operation();
                var result = await _context.SaveChangesAsync(CancellationToken.None);
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Ngăn GC gọi finalizer
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Giải phóng các tài nguyên quản lý
                    _context?.Dispose();
                }

                // Giải phóng tài nguyên không quản lý (nếu có)

                _disposed = true;
            }
        }
    }
}
