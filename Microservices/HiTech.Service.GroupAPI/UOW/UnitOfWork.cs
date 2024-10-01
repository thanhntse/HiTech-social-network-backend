using HiTech.Service.GroupAPI.Data;
using HiTech.Service.GroupAPI.Repositories;

namespace HiTech.Service.GroupAPI.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;

        private readonly GroupDbContext _context;
        public IGroupRepository Groups { get; private set; }
        public IGroupUserRepository GroupUsers { get; private set; }
        public IUserRepository Users { get; private set; }
        public IJoinRequestRepository JoinRequests { get; private set; }
        public UnitOfWork(GroupDbContext context)
        {
            _context = context;
            Groups = new GroupRepository(context);
            GroupUsers = new GroupUserRepository(context);
            Users = new UserRepository(context);
            JoinRequests = new JoinRequestRepository(context);
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
