using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.GroupAPI.Entities
{
    [Table("group", Schema = "dbo")]
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("group_id")]
        public int GroupId { get; set; }

        [Column("group_name")]
        [MaxLength(100)]
        public string GroupName { get; set; } = null!;

        [Column("description")]
        public string? Description { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("founder_id")]
        public int FounderId { get; set; }

        public virtual User Founder { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
