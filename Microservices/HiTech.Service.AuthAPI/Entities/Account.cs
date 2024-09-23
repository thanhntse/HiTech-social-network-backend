using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        public string Email { get; set; } = null!;

        [Column("password")]
        public string Password { get; set; } = null!;

        [Column("full_name")]
        public string FullName { get; set; } = null!;

        [Column("bio")]
        public string? Bio { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("role")]
        public string Role { get; set; } = "Member";

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [JsonIgnore]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        [JsonIgnore]
        public virtual ICollection<ExpiredToken> ExpiredTokens { get; set; } = new List<ExpiredToken>();
    }
}
