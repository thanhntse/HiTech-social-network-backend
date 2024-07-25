using API.Blog.Entities;

namespace API.Blog.Repositories
{
    public class BlogTagRepository : IBlogTagRepository
    {
        public Task AddAsync(BlogTag blogTag)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(BlogTag blogTag)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogTag>> GetAllByBlogIdAsync(int blogId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BlogTag>> GetAllByTagIdAsync(int tagId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(BlogTag blogTag)
        {
            throw new NotImplementedException();
        }
    }
}
