using HiTech.Service.AuthAPI.Data;
using HiTech.Service.AuthAPI.Repositories;

namespace HiTech.Service.AuthAPI.UOW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthDbContext _context;
        public IAccountRepository Accounts { get; private set; }
        public IRefeshTokenRepository RefeshTokens { get; private set; }
        public IExpiredTokenRepository ExpiredTokens { get; private set; }

        public UnitOfWork(AuthDbContext context)
        {
            _context = context;
            Accounts = new AccountRepository(context);
            RefeshTokens = new RefeshTokenRepository(context);
            ExpiredTokens = new ExpiredTokenRepository(context);
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
            _context.Dispose();
        }
    }

}
