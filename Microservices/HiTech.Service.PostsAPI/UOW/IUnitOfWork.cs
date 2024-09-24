using HiTech.Service.PostsAPI.Repositories;

namespace HiTech.Service.PostsAPI.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IPostRepository Posts { get; }
        ICommentRepository Comments { get; }
        ILikeRepository Likes { get; }
        IImageRepository Images { get; }
        Task<int> SaveAsync();
        Task<int> SaveWithTransactionAsync();
        Task<int> SaveWithTransactionAsync(Func<Task> operation);
    }
}
