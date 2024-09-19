using System.ComponentModel.DataAnnotations.Schema;

namespace HiTech.Service.AccountAPI.DTOs.Response
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}
