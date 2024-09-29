namespace HiTech.Service.FriendAPI.DTOs.Response
{
    public class FriendRequestResponse
    {
        public int FriendRequestId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        public virtual UserResponse User { get; set; } = null!;
    }
}
