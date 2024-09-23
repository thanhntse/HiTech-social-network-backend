using HiTech.Service.AuthAPI.Data;
using HiTech.Service.AuthAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.AuthAPI.Repositories
{
    public interface IRefeshTokenRepository : IGenericRepository<RefreshToken, int>
    {
        Task<RefreshToken?> GetByRefreshTokenAsync(string token);
    }

    public sealed class RefeshTokenRepository
        : GenericRepository<AuthDbContext, RefreshToken, int>, IRefeshTokenRepository
    {
        public RefeshTokenRepository(AuthDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken?> GetByRefreshTokenAsync(string token)
            => await DbSet.FirstOrDefaultAsync(t => t.Token == token);
    }
}
