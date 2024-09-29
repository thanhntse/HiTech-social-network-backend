using HiTech.Service.AuthAPI.Repositories;

namespace HiTech.Service.AuthAPI.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        IAccountInfoRepository AccountInfos { get; }
        IRefeshTokenRepository RefeshTokens { get; }
        IExpiredTokenRepository ExpiredTokens { get; }
        Task<int> SaveAsync();
        Task<int> SaveWithTransactionAsync();
        Task<int> SaveWithTransactionAsync(Func<Task> operation);
    }

}
