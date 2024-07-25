using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebAPI.DTO.Response
{
    public class CommentResponse
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int AuthorId { get; set; }
    }
}
