using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HiTech.Service.AuthAPI.Entities
{
    [Table("expired_token", Schema = "dbo")]
    public class ExpiredToken
    {
        [Key]
        [Column("token")]
        public string Token { get; set; } = null!;

        [Column("invalidation_time")]
        public DateTime InvalidationTime { get; set; }

        [Column("account_id")]
        public int AccountId { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
