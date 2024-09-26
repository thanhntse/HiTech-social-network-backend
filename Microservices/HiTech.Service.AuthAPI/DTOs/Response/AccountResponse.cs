namespace HiTech.Service.AuthAPI.DTOs.Response
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Bio { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? Background { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; } = null!;
        public bool IsDeleted { get; set; } = false;
    }
}
