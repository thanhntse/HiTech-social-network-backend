using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.FriendAPI.Entities
{
    [Table("friend_request", Schema = "dbo")]
    public class FriendRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("friend_request_id")]
        public int FriendRequestId { get; set; }

        [Column("sender_id")]
        public int SenderId { get; set; }

        [Column("receiver_id")]
        public int ReceiverId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("status")]
        public string Status { get; set; } = "Pending";

        public virtual User Sender { get; set; } = null!;

        public virtual User Receiver { get; set; } = null!;
    }
}
