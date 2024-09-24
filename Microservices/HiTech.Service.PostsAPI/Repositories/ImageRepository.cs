using HiTech.Service.PostsAPI.Data;
using HiTech.Service.PostsAPI.Entities;
using HiTech.Shared.EF.Repositories;
using HiTech.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.PostsAPI.Repositories
{
    public interface IImageRepository : IGenericRepository<Image, int>
    {
    }

    public sealed class ImageRepository
        : GenericRepository<PostDbContext, Image, int>, IImageRepository
    {
        public ImageRepository(PostDbContext context) : base(context)
        {
        }
    }
}
