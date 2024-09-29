using HiTech.Service.FriendAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.FriendAPI.Data
{
    public class FriendDbContext : DbContext
    {
        public FriendDbContext(DbContextOptions<FriendDbContext> options) : base(options)
        {
            //try
            //{
            //    var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            //    if (databaseCreator != null)
            //    {
            //        if (!databaseCreator.CanConnect()) databaseCreator.Create();
            //        if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        public virtual DbSet<FriendRequest> FriendRequests { get; set; }
        public virtual DbSet<Friendship> Friendships { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FriendRequest>()
                .HasOne(c => c.Sender)
                .WithMany(p => p.SentRequests)
                .HasForeignKey(c => c.SenderId);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(c => c.Receiver)
                .WithMany(p => p.ReceivedRequests)
                .HasForeignKey(c => c.ReceiverId);

            modelBuilder.Entity<Friendship>()
                .HasOne(c => c.UserSent)
                .WithMany(p => p.FriendshipsSent)
                .HasForeignKey(c => c.UserSentId);

            modelBuilder.Entity<Friendship>()
                .HasOne(c => c.UserReceived)
                .WithMany(p => p.FriendshipsReceived)
                .HasForeignKey(c => c.UserReceivedId);

            modelBuilder.Entity<FriendRequest>()
                .ToTable(b =>
                {
                    b.HasCheckConstraint("CK_Status_Valid", "[status] IN ('Pending', 'Accepted', 'Denied')");
                });
        }
    }
}
