namespace HiTech.Service.GroupAPI.DTOs.Response
{
    public class GroupResponse
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual UserResponse Founder { get; set; } = null!;
    }
}
