namespace HiTech.Service.PostsAPI.DTOs.Request
{
    public class CommentRequest
    {
        public string Content { get; set; } = null!;
        public int PostId { get; set; }
    }
}
