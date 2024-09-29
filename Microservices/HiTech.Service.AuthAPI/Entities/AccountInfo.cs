using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HiTech.Service.AuthAPI.Entities
{
    [Table("account_info", Schema = "dbo")]
    public class AccountInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("account_info_id")]
        public int AccountInfoId { get; set; }

        [Column("bio")]
        public string? Bio { get; set; }

        [Column("phone")]
        [MaxLength(10)]
        public string? Phone { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("background")]
        public string? Background { get; set; }

        [Column("dob")]
        public DateOnly? Dob { get; set; }

        [Column("other_info")]
        public string? OtherInfo { get; set; }

        [JsonIgnore]
        [Column("account_id")]
        public int AccountId { get; set; }

        [JsonIgnore]
        public virtual Account Account { get; set; } = null!;
    }
}
