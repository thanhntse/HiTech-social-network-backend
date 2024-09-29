using HiTech.Service.FriendAPI.Data;
using HiTech.Service.FriendAPI.Repositories;

namespace HiTech.Service.FriendAPI.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;

        private readonly FriendDbContext _context;
        public IFriendRequestRepository FriendRequests { get; private set; }
        public IFriendshipRepository Friendships { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(FriendDbContext context)
        {
            _context = context;
            FriendRequests = new FriendRequestRepository(context);
            Friendships = new FriendshipRepository(context);
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
