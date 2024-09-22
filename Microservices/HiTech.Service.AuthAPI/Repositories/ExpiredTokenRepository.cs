using HiTech.Service.AuthAPI.Data;
using HiTech.Service.AuthAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.AuthAPI.Repositories
{
    public interface IExpiredTokenRepository : IGenericRepository<ExpiredToken, string>
    {
    }

    public class ExpiredTokenRepository
        : GenericRepository<AuthDbContext, ExpiredToken, string>, IExpiredTokenRepository
    {
        public ExpiredTokenRepository(AuthDbContext context) : base(context)
        {
        }
    }
}
