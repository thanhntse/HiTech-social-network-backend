namespace HiTech.Service.NotificationAPI.DTOs.Response
{
    public class NotificationResponse
    {
        public int NotificationId { get; set; }
        public string Type { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
    }
}
