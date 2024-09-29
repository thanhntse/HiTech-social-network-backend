using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HiTech.Service.AuthAPI.Entities
{
    [Table("account", Schema = "dbo")]
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("account_id")]
        public int AccountId { get; set; }

        [Column("email")]
        [MaxLength(30)]
        public string Email { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;

        [Column("full_name")]
        [MaxLength(50)]
        public string FullName { get; set; } = null!;

        [Column("avatar")]
        public string? Avatar { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("role")]
        public string Role { get; set; } = "Member";

        [Column("last_login")]
        public DateTime? LastLogin { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = false;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        public virtual AccountInfo AccountInfo { get; set; } = null!;

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        public virtual ICollection<ExpiredToken> ExpiredTokens { get; set; } = new List<ExpiredToken>();
    }
}
