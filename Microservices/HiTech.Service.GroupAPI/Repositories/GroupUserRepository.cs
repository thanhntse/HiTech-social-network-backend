using HiTech.Service.GroupAPI.Data;
using HiTech.Service.GroupAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.GroupAPI.Repositories
{
    public interface IGroupUserRepository : IGenericRepository<GroupUser, int>
    {
    }

    public sealed class GroupUserRepository
        : GenericRepository<GroupDbContext, GroupUser, int>, IGroupUserRepository
    {
        public GroupUserRepository(GroupDbContext context) : base(context)
        {
        }
    }
}
