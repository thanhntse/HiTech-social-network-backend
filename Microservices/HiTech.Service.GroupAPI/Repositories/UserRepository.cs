using HiTech.Service.GroupAPI.Data;
using HiTech.Service.GroupAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.GroupAPI.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
    }

    public sealed class UserRepository
        : GenericRepository<GroupDbContext, User, int>, IUserRepository
    {
        public UserRepository(GroupDbContext context) : base(context)
        {
        }
    }
}
