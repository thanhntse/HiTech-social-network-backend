namespace HiTech.Service.PostsAPI.Services.IService
{
    public interface ILikeService
    {
        Task<IEnumerable<int>> GetAllAuthorIDByPostIDAsync(int postId);
        Task<bool> CreateAsync(string authorId, int postId);
        Task<bool> DeleteAsync(string authorId, int postId);
    }
}
