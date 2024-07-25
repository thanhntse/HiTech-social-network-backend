using System.ComponentModel.DataAnnotations.Schema;

namespace API.Blog.DTO.Request
{
    public class BlogRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public List<string> Tags { get; set; } = null!;
    }
}
