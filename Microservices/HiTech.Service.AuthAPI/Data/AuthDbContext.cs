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

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<ExpiredToken> ExpiredTokens { get; set; }
    }
}
