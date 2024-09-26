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
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = null!;

        [Column("password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = null!;

        [Column("full_name")]
        [MinLength(6, ErrorMessage = "Full name must be at least 6 characters long.")]
        public string FullName { get; set; } = null!;

        [Column("bio")]
        public string? Bio { get; set; }

        [Column("phone")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone number must be exactly 10 digits.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must start with 0 and be 10 digits long.")]
        public string? Phone { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("avatar")]
        public string? Avatar { get; set; }

        [Column("background")]
        public string? Background { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Column("role")]
        [RegularExpression("^(Member|Admin)$", ErrorMessage = "Role must be either 'Member' or 'Admin'.")]
        public string Role { get; set; } = "Member";

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [JsonIgnore]
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

        [JsonIgnore]
        public virtual ICollection<ExpiredToken> ExpiredTokens { get; set; } = new List<ExpiredToken>();
    }
}
