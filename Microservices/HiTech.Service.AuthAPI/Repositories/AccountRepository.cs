using HiTech.Service.AuthAPI.Data;
using HiTech.Service.AuthAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.AuthAPI.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account, int>
    {
        Task<Account?> GetByEmailAsync(string email);
    }

    public sealed class AccountRepository
        : GenericRepository<AuthDbContext, Account, int>, IAccountRepository
    {
        public AccountRepository(AuthDbContext context) : base(context)
        {
        }

        public async Task<Account?> GetByEmailAsync(string email)
            => await _dbSet.FirstOrDefaultAsync(a => a.Email == email);
    }
}
