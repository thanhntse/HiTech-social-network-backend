using HiTech.Service.PostsAPI.Data;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;

namespace HiTech.Service.PostsAPI.Repositories
{
    public interface IUserRepository : IGenericRepository<User, int>
    {
    }

    public sealed class UserRepository
        : GenericRepository<PostDbContext, User, int>, IUserRepository
    {
        public UserRepository(PostDbContext context) : base(context)
        {
        }
    }
}
