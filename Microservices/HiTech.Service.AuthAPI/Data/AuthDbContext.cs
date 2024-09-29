using HiTech.Service.AuthAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.AuthAPI.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
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

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountInfo> AccountInfos { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<ExpiredToken> ExpiredTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasOne(a => a.AccountInfo)
                .WithOne(a => a.Account)
                .HasForeignKey<AccountInfo>(a => a.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.Account)
                .WithMany(a => a.RefreshTokens)
                .HasForeignKey(rt => rt.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExpiredToken>()
                .HasOne(rt => rt.Account)
                .WithMany(a => a.ExpiredTokens)
                .HasForeignKey(rt => rt.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasIndex(a => a.Email)
                .IsUnique();

            modelBuilder.Entity<Account>()
                .ToTable(b =>
                {
                    b.HasCheckConstraint("CK_Email_Length", "LEN([email]) >= 6");
                    b.HasCheckConstraint("CK_Email_Valid", "CHARINDEX('@', [email]) > 0");
                    b.HasCheckConstraint("CK_Fullname_Length", "LEN([full_name]) >= 6");
                    b.HasCheckConstraint("CK_Role_Valid", "[role] IN ('Member', 'Admin')");
                });

            modelBuilder.Entity<AccountInfo>()
                .ToTable(b =>
                {
                    b.HasCheckConstraint("CK_Phone_Valid", "LEN([phone]) = 10");
                });
        }
    }
}
