using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Blog.Entities
{
    [Table("blog", Schema = "dbo")]
    public class Blog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("blog_id")]
        public int BlogId { get; set; }

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime UpdateAt { get; set; }

        [Column("published_at")]
        public DateTime PublishedAt { get; set; }

        [Column("like")]
        public int Like { get; set; } = 0;

        [Column("comments_count")]
        public int CommentsCount { get; set; } = 0;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("author_id")]
        public int AuthorId { get; set; }

        public ICollection<BlogTag> BlogTags { get; set; } = new List<BlogTag>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
