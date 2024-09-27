using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.NotificationAPI.Entities
{
    [Table("notification", Schema = "dbo")]
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("notification_id")]
        public int NotificationId { get; set; }

        [Column("type")]
        public string Type { get; set; } = null!;

        [Column("content")]
        [MaxLength(200)]
        public string Content { get; set; } = null!;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("is_read")]
        public bool IsRead { get; set; } = false;

        [Column("user_id")]
        public int UserId { get; set; }
    }
}
