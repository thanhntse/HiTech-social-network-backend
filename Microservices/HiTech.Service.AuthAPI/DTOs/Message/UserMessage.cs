namespace HiTech.Service.AuthAPI.DTOs.Message
{
    public class UserMessage
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Avatar { get; set; }
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
