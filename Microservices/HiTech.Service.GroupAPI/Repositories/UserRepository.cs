using HiTech.Service.GroupAPI.Data;
using HiTech.Service.GroupAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.GroupAPI.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
        Task<IEnumerable<Group>> GetAllGroupByUserIDAsync(int userId);
    }

    public sealed class UserRepository
        : GenericRepository<GroupDbContext, User, int>, IUserRepository
    {
        public UserRepository(GroupDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Group>> GetAllGroupByUserIDAsync(int userId)
            => await _dbSet.Where(i => i.UserId == userId)
                                     .Include(i => i.Groups)
                                     .Select(i => i.Groups)
                                     .FirstOrDefaultAsync() ?? [];
    }
}
