namespace HiTech.Service.PostsAPI.DTOs.Response
{
    public class PostResponse
    {
        public int PostId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public int Like { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;
        public virtual UserResponse User { get; set; } = null!;
        public virtual ICollection<ImageResponse> Images { get; set; } = new List<ImageResponse>();
    }
}
