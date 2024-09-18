using HiTech.Service.BlogAPI.DTOs.Response;
using HiTech.Service.BlogAPI.Entities;

namespace HiTech.Service.BlogAPI.Services
{
    public interface ITagService
    {
        Task<IEnumerable<TagResponse>> GetAllTagsAsync();
        Task<TagResponse> GetTagByIdAsync(int id);
    }
}
