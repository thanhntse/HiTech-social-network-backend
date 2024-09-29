using HiTech.Service.AuthAPI.Data;
using HiTech.Service.AuthAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.AuthAPI.Repositories
{
    public interface IAccountInfoRepository : IGenericRepository<AccountInfo, int>
    {
    }

    public sealed class AccountInfoRepository
        : GenericRepository<AuthDbContext, AccountInfo, int>, IAccountInfoRepository
    {
        public AccountInfoRepository(AuthDbContext context) : base(context)
        {
        }
    }
}
