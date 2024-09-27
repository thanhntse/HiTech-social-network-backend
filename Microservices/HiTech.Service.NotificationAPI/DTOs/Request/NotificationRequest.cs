namespace HiTech.Service.NotificationAPI.DTOs.Request
{
    public class NotificationRequest
    {
        public string Type { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
    }
}
