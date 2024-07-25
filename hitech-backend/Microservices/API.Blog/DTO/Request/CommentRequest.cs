using API.Blog.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Blog.DTO.Request
{
    public class CommentRequest
    {
        public string Content { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int BlogId { get; set; }
    }
}
