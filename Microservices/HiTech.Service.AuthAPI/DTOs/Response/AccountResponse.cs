using HiTech.Service.AuthAPI.Entities;

namespace HiTech.Service.AuthAPI.DTOs.Response
{
    public class AccountResponse
    {
        public int AccountId { get; set; }
        public string Email { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string? Avatar { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public virtual AccountInfo AccountInfo { get; set; } = null!;

    }
}
