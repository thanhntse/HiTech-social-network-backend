using HiTech.Service.FriendAPI.Data;
using HiTech.Service.FriendAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.FriendAPI.Repositories
{
    public interface IFriendRequestRepository : IGenericRepository<FriendRequest, int>
    {
        Task<bool> FriendRequestExists(int senderId, int receiverId);
        Task<IEnumerable<FriendRequest>> GetAllReceivedRequestsAsync(int userId);
        Task<IEnumerable<FriendRequest>> GetAllSentRequestsAsync(int userId);
    }

    public sealed class FriendRequestRepository
        : GenericRepository<FriendDbContext, FriendRequest, int>, IFriendRequestRepository
    {
        public FriendRequestRepository(FriendDbContext context) : base(context)
        {
        }

        public async Task<bool> FriendRequestExists(int senderId, int receiverId)
            => await _dbSet.AnyAsync(
                fr => (fr.SenderId == senderId && fr.ReceiverId == receiverId)
                   || (fr.SenderId == receiverId && fr.ReceiverId == senderId)
            );

        public async Task<IEnumerable<FriendRequest>> GetAllReceivedRequestsAsync(int userId)
            => await _dbSet.Include(fr => fr.Sender)
                           .Where(fr => fr.ReceiverId == userId)
                           .ToListAsync();

        public async Task<IEnumerable<FriendRequest>> GetAllSentRequestsAsync(int userId)
            => await _dbSet.Include(fr => fr.Receiver)
                           .Where(fr => fr.SenderId == userId)
                           .ToListAsync();
    }
}
