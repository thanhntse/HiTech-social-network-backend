using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HiTech.Service.AuthAPI.Entities
{
    [Table("refresh_token", Schema = "dbo")]
    public class RefreshToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("refresh_token_id")]
        public int RefreshTokenId { get; set; }

        [Column("token")]
        public string Token { get; set; } = null!;

        [Column("expiry")]
        public DateTime Expiry { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("revoked")]
        public DateTime? Revoked { get; set; }

        [Column("account_id")]
        public int AccountId { get; set; }

        [NotMapped]
        public bool IsExpired => DateTime.UtcNow >= Expiry;

        [NotMapped]
        public bool IsActive => Revoked == null && !IsExpired;

        public virtual Account Account { get; set; } = null!;
    }
}
