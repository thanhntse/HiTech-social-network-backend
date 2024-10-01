using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.GroupAPI.Entities
{
    [Table("join_request", Schema = "dbo")]
    public class JoinRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("join_request_id")]
        public int JoinRequestId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("status")]
        public string Status { get; set; } = "Pending";

        public virtual Group Group { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
