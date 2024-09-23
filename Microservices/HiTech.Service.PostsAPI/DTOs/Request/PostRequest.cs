namespace HiTech.Service.PostsAPI.DTOs.Request
{
    public class PostRequest
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public virtual ICollection<string> Images { get; set; } = new List<string>();
    }
}
