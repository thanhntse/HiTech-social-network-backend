namespace HiTech.Service.AccountAPI.DTOs.Request
{
    public class AccountRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? FirstName { get; set; } = null!;

        public string? LastName { get; set; } = null!;

        public string? Phone { get; set; } = null!;

        public string? Address { get; set; } = null!;
    }
}
