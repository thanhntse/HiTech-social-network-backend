using HiTech.Service.GroupAPI.Data;
using HiTech.Service.GroupAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.GroupAPI.Repositories
{
    public interface IGroupRepository : IGenericRepository<Group, int>
    {
    }

    public sealed class GroupRepository
        : GenericRepository<GroupDbContext, Group, int>, IGroupRepository
    {
        public GroupRepository(GroupDbContext context) : base(context)
        {
        }
    }
}
