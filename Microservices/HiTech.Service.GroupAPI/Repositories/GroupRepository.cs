using HiTech.Service.GroupAPI.Data;
using HiTech.Service.GroupAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.GroupAPI.Repositories
{
    public interface IGroupRepository : IGenericRepository<Group, int>
    {
        Task<Group?> GetDetailByIDAsync(int id);
        Task<IEnumerable<User>> GetAllUserByGroupIDAsync(int id);
    }

    public sealed class GroupRepository
        : GenericRepository<GroupDbContext, Group, int>, IGroupRepository
    {
        public GroupRepository(GroupDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllUserByGroupIDAsync(int id)
            => await _dbSet.Where(g => g.GroupId == id)
                                    .Include(g => g.Users)
                                    .Select(g => g.Users)
                                    .FirstOrDefaultAsync() ?? [];

        public async Task<Group?> GetDetailByIDAsync(int id)
            => await _dbSet.Include(i => i.Founder)
                           .FirstOrDefaultAsync(i => i.GroupId == id);
    }
}
