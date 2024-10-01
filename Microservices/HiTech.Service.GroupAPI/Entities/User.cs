﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HiTech.Service.GroupAPI.Entities
{
    [Table("user", Schema = "dbo")]
    public class User
    {
        [Key]
        [Column("user_id")]
        public int UserId { get; set; }

        [Column("full_name")]
        public string FullName { get; set; } = null!;

        [Column("avatar")]
        public string? Avatar { get; set; }

        [Column("last_login")]
        public DateTime? LastLogin { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = false;

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Group> MyGroups { get; set; } = new List<Group>();

        public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

        public virtual ICollection<JoinRequest> JoinRequests { get; set; } = new List<JoinRequest>();
    }
}
