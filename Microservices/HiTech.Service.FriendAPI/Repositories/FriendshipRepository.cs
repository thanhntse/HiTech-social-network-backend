using HiTech.Service.FriendAPI.Data;
using HiTech.Service.FriendAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.FriendAPI.Repositories
{
    public interface IFriendshipRepository : IGenericRepository<Friendship, int>
    {
        Task<IEnumerable<User>> GetAllFriendsAsync(int userId);
        Task<IEnumerable<Friendship>> GetAllFriendsDetailAsync(int userId);
    }

    public sealed class FriendshipRepository
        : GenericRepository<FriendDbContext, Friendship, int>, IFriendshipRepository
    {
        public FriendshipRepository(FriendDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetAllFriendsAsync(int userId)
        {
            var friendsSent = await _dbSet
               //.Where(f => f.UserSentId == userId && f.Status == true)
               .Where(f => f.UserSentId == userId)
               .Include(f => f.UserReceived)
               .Select(f => f.UserReceived)
               .ToListAsync();

            var friendsReceived = await _dbSet
                //.Where(f => f.UserReceivedId == userId && f.Status == true)
                .Where(f => f.UserReceivedId == userId)
                .Include(f => f.UserSent)
                .Select(f => f.UserSent)
                .ToListAsync();

            return friendsSent.Concat(friendsReceived).Distinct();
        }

        public async Task<IEnumerable<Friendship>> GetAllFriendsDetailAsync(int userId)
        {
            var friendshipSent = await _dbSet
               //.Where(f => f.UserSentId == userId && f.Status == true)
               .Where(f => f.UserSentId == userId)
               .Include(f => f.UserReceived)
               .ToListAsync();

            var friendshipReceived = await _dbSet
                //.Where(f => f.UserReceivedId == userId && f.Status == true)
                .Where(f => f.UserReceivedId == userId)
                .Include(f => f.UserSent)
                .ToListAsync();

            return friendshipSent.Concat(friendshipReceived).Distinct();
        }
    }
}
