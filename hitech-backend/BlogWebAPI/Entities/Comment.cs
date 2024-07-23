using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebAPI.Entities
{
    [Table("comment", Schema = "dbo")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("comment_id")]
        public int CommentId { get; set; }

        [Column("content")]
        public string Content { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdateAt { get; set; }

        [Column("author_id")]
        public int AuthorId { get; set; }

        [Column("blog_id")]
        public int BlogId { get; set; }

        public Blog Blog { get; set; } = null!;
    }
}
