using HiTech.Service.FriendAPI.Data;
using HiTech.Service.FriendAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.FriendAPI.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
    }

    public sealed class UserRepository
        : GenericRepository<FriendDbContext, User, int>, IUserRepository
    {
        public UserRepository(FriendDbContext context) : base(context)
        {
        }
    }
}
