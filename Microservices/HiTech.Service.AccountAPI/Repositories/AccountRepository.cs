using HiTech.Service.AccountAPI.Data;
using HiTech.Service.AccountAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.AccountAPI.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account, int>
    {
        Task<Account?> GetByEmailAsync(string email);
    }

    public sealed class AccountRepository
        : GenericRepository<AccountDbContext, Account, int>, IAccountRepository
    {
        public AccountRepository(AccountDbContext context) : base(context)
        {
        }

        public async Task<Account?> GetByEmailAsync(string email) => await DbSet.FirstOrDefaultAsync(a => a.Email == email);
    }
}
