using HiTech.Service.PostsAPI.Data;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.PostsAPI.Repositories
{
    public interface ILikeRepository : IGenericRepository<Like, int>
    {
    }

    public sealed class LikeRepository
        : GenericRepository<PostDbContext, Like, int>, ILikeRepository
    {
        public LikeRepository(PostDbContext context) : base(context)
        {
        }
    }
}
