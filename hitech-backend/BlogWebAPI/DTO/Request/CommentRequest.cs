using BlogWebAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebAPI.DTO.Request
{
    public class CommentRequest
    {
        public string Content { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int BlogId { get; set; }
    }
}
