namespace HiTech.Service.AuthAPI.DTOs.Request
{
    public class AccountCreationRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FullName { get; set; } = null!;
    }
}
