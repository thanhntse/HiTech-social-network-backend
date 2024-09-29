using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.FriendAPI.Entities
{
    [Table("friendship", Schema = "dbo")]
    public class Friendship
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("friendship_id")]
        public int FriendshipId { get; set; }

        [Column("user_sent_id")]
        public int UserSentId { get; set; }

        [Column("user_received_id")]
        public int UserReceivedId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("status")]
        public bool Status { get; set; } = true;

        public virtual User UserSent { get; set; } = null!;

        public virtual User UserReceived { get; set; } = null!;
    }
}
