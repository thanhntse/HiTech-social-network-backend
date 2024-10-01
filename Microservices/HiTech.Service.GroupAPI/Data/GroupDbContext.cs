using Azure;
using HiTech.Service.GroupAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.GroupAPI.Data
{
    public class GroupDbContext : DbContext
    {
        public GroupDbContext(DbContextOptions<GroupDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<GroupUser> GroupUsers { get; set; }
        public virtual DbSet<JoinRequest> JoinRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Users)
                .WithMany(u => u.Groups)
                .UsingEntity<GroupUser>(
                    l => l.HasOne<User>().WithMany().HasForeignKey(e => e.UserId),
                    r => r.HasOne<Group>().WithMany().HasForeignKey(e => e.GroupId)
                );

            modelBuilder.Entity<Group>()
                .HasOne(g => g.Founder)
                .WithMany(f => f.MyGroups)
                .HasForeignKey(f => f.FounderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JoinRequest>()
                .HasOne(g => g.Group)
                .WithMany(f => f.JoinRequests)
                .HasForeignKey(f => f.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JoinRequest>()
                .HasOne(g => g.User)
                .WithMany(f => f.JoinRequests)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
