using BlogWebAPI.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebAPI.DTO.Response
{
    public class BlogResponse
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public DateTime PublishedAt { get; set; }
        public int Like { get; set; } = 0;
        public int CommentsCount { get; set; } = 0;
        public int AuthorId { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
