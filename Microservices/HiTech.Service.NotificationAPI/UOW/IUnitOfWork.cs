using HiTech.Service.NotificationAPI.Repositories;

namespace HiTech.Service.NotificationAPI.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        INotificationRepository Notifications { get; }
        Task<int> SaveAsync();
        Task<int> SaveWithTransactionAsync();
        Task<int> SaveWithTransactionAsync(Func<Task> operation);
    }
}
