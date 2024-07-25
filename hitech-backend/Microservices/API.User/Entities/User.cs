using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.User.Entities
{
    [Table("user", Schema = "dbo")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("password")]
        public string Password { get; set; } = string.Empty;

        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [Column("address")]
        public string Address { get; set; } = string.Empty;

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("role")]
        public string Role { get; set; } = string.Empty;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;
    }
}
