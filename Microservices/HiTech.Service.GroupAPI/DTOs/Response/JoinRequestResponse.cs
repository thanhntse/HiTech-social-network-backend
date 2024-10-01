namespace HiTech.Service.GroupAPI.DTOs.Response
{
    public class JoinRequestResponse
    {
        public int JoinRequestId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        public virtual UserResponse User { get; set; } = null!;
    }
}
