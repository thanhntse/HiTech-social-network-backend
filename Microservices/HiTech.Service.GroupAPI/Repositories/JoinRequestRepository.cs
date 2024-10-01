using HiTech.Service.GroupAPI.Data;
using HiTech.Service.GroupAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.GroupAPI.Repositories
{
    public interface IJoinRequestRepository : IGenericRepository<JoinRequest, int>
    {
    }

    public sealed class JoinRequestRepository
        : GenericRepository<GroupDbContext, JoinRequest, int>, IJoinRequestRepository
    {
        public JoinRequestRepository(GroupDbContext context) : base(context)
        {
        }
    }
}
