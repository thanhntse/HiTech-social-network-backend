using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HiTech.Service.PostsAPI.Entities
{
    [Table("comment", Schema = "dbo")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("comment_id")]
        public int CommentId { get; set; }

        [Column("content")]
        public string Content { get; set; } = null!;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("updated_at")]
        public DateTime? UpdateAt { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("author_id")]
        public int AuthorId { get; set; }

        [Column("post_id")]
        public int PostId { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual Post Post { get; set; } = null!;
    }
}
