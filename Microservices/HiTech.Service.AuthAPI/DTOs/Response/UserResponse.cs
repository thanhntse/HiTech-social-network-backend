namespace HiTech.Service.AuthAPI.DTOs.Response
{
    public class UserResponse
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Avatar { get; set; } = null!;
    }
}
