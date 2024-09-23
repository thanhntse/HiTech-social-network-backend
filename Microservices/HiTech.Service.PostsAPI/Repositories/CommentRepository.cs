using HiTech.Service.PostsAPI.Data;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.PostsAPI.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment, int>
    {
    }

    public sealed class CommentRepository
        : GenericRepository<PostDbContext, Comment, int>, ICommentRepository
    {
        public CommentRepository(PostDbContext context) : base(context)
        {
        }
    }
}
