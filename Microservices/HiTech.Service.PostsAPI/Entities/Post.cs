using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HiTech.Service.PostsAPI.Entities
{
    [Table("post", Schema = "dbo")]
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("post_id")]
        public int PostId { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("content")]
        public string? Content { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdateAt { get; set; }

        [Column("published_at")]
        public DateTime? PublishedAt { get; set; }

        [Column("like")]
        public int Like { get; set; } = 0;

        [Column("comments_count")]
        public int CommentsCount { get; set; } = 0;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("author_id")]
        public int AuthorId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
