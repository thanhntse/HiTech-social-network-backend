using HiTech.Service.FriendAPI.Repositories;

namespace HiTech.Service.FriendAPI.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IFriendRequestRepository FriendRequests { get; }
        IFriendshipRepository Friendships { get; }
        IUserRepository Users { get; }
        Task<int> SaveAsync();
        Task<int> SaveWithTransactionAsync();
        Task<int> SaveWithTransactionAsync(Func<Task> operation);
    }
}
