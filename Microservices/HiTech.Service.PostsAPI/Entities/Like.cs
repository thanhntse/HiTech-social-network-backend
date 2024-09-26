using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.PostsAPI.Entities
{
    [Table("like", Schema = "dbo")]
    public class Like
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("like_id")]
        public int LikeId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("author_id")]
        public int AuthorId { get; set; }

        [Column("post_id")]
        public int PostId { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual Post Post { get; set; } = null!;
    }
}
