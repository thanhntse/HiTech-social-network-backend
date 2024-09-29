namespace HiTech.Service.FriendAPI.DTOs.Response
{
    public class FriendshipResponse
    {
        public int FriendshipId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool Status { get; set; } = true;
        public virtual UserResponse User { get; set; } = null!;
    }
}
