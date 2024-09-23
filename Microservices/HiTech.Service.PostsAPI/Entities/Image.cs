using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.PostsAPI.Entities
{
    [Table("image", Schema = "dbo")]
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("image_id")]
        public int ImageId { get; set; }

        [Column("image_no")]
        public int ImageNo { get; set; }

        [Column("image_url")]
        public string ImageUrl { get; set; } = null!;

        [Column("post_id")]
        public int PostId { get; set; }

        public virtual Post Post { get; set; } = null!;
    }
}
