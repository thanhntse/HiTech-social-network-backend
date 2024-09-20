using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.AccountAPI.DTOs.Response
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; } = null!;
        public string? LastName { get; set; } = null!;
        public string? Phone { get; set; } = null!;
        public string? Address { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
    }
}
