using HiTech.Service.GroupAPI.Repositories;

namespace HiTech.Service.GroupAPI.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IGroupRepository Groups { get; }
        IGroupUserRepository GroupUsers { get; }
        IUserRepository Users { get; }
        Task<int> SaveAsync();
        Task<int> SaveWithTransactionAsync();
        Task<int> SaveWithTransactionAsync(Func<Task> operation);
    }
}
