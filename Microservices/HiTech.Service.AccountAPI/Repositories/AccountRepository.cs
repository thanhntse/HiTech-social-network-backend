using HiTech.Service.AccountAPI.Data;
using HiTech.Service.AccountAPI.Entities;
using HiTech.Shared.EF;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.AccountAPI.Repositories
{
    public interface IAccountRepository : IGenericRepository<Account, int>
    {
    }

    public sealed class AccountRepository
        : GenericDbContextRepository<AccountDbContext, Account, int>, IAccountRepository
    {
        public AccountRepository(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }
    }
}
