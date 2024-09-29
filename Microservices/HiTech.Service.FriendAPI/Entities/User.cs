using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HiTech.Service.FriendAPI.Entities
{
    [Table("user", Schema = "dbo")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("full_name")]
        public string FullName { get; set; } = null!;

        [Column("avatar")]
        public string? Avatar { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Friendship> FriendshipsSent { get; set; } = new List<Friendship>();

        public virtual ICollection<Friendship> FriendshipsReceived { get; set; } = new List<Friendship>();

        public virtual ICollection<FriendRequest> SentRequests { get; set; } = new List<FriendRequest>();

        public virtual ICollection<FriendRequest> ReceivedRequests { get; set; } = new List<FriendRequest>();
    }
}
