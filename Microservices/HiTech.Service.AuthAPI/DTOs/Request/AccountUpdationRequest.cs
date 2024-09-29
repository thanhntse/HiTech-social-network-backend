namespace HiTech.Service.AuthAPI.DTOs.Request
{
    public class AccountUpdationRequest
    {
        public string FullName { get; set; } = null!;
        public string? Bio { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? Background { get; set; }
        public DateOnly? Dob { get; set; }
        public string? OtherInfo { get; set; }
    }
}
