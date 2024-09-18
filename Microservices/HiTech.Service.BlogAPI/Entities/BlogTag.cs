using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.BlogAPI.Entities
{
    [Table("blog_tag", Schema = "dbo")]
    public class BlogTag
    {
        [Column("blog_id")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;

        [Column("tag_id")]
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
    }

}
