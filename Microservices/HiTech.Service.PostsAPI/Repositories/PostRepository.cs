using HiTech.Service.PostsAPI.Data;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.PostsAPI.Repositories
{
    public interface IPostRepository : IGenericRepository<Post, int>
    {
    }

    public sealed class PostRepository
        : GenericRepository<PostDbContext, Post, int>, IPostRepository
    {
        public PostRepository(PostDbContext context) : base(context)
        {
        }
    }
}
