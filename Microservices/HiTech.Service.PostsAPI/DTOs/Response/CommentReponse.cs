namespace HiTech.Service.PostsAPI.DTOs.Response
{
    public class CommentReponse
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdateAt { get; set; }
        public virtual UserResponse User { get; set; } = null!;
    }
}
