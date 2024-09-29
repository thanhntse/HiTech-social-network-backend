using HiTech.Service.FriendAPI.Data;
using HiTech.Service.FriendAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.FriendAPI.Repositories
{
    public interface IFriendshipRepository : IGenericRepository<Friendship, int>
    {
    }

    public sealed class FriendshipRepository
        : GenericRepository<FriendDbContext, Friendship, int>, IFriendshipRepository
    {
        public FriendshipRepository(FriendDbContext context) : base(context)
        {
        }
    }
}
