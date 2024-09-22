namespace HiTech.Service.AuthAPI.DTOs.Message
{
    public class LoginResponseMessage
    {
        public bool Success { get; set; }
        public int AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
