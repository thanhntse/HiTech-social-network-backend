using API.Blog.DTO.Response;
using API.Blog.Entities;

namespace API.Blog.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagResponse>> GetAllTagsAsync();
        Task<TagResponse> GetTagByIdAsync(int id);
    }
}
