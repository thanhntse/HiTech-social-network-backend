using HiTech.Service.FriendAPI.Data;
using HiTech.Service.FriendAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.FriendAPI.Repositories
{
    public interface IFriendRequestRepository : IGenericRepository<FriendRequest, int>
    {
    }

    public sealed class FriendRequestRepository
        : GenericRepository<FriendDbContext, FriendRequest, int>, IFriendRequestRepository
    {
        public FriendRequestRepository(FriendDbContext context) : base(context)
        {
        }
    }
}
