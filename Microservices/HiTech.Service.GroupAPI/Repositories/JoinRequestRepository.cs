using HiTech.Service.GroupAPI.Data;
using HiTech.Service.GroupAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.GroupAPI.Repositories
{
    public interface IJoinRequestRepository : IGenericRepository<JoinRequest, int>
    {
        Task<JoinRequest?> GetDetailByIDAsync(int id);
    }

    public sealed class JoinRequestRepository
        : GenericRepository<GroupDbContext, JoinRequest, int>, IJoinRequestRepository
    {
        public JoinRequestRepository(GroupDbContext context) : base(context)
        {
        }

        public async Task<JoinRequest?> GetDetailByIDAsync(int id)
        {
            return await _dbSet.Include(r => r.Group)
                               .Include(r => r.User)
                               .FirstOrDefaultAsync(r => r.JoinRequestId == id);
        }
    }
}
