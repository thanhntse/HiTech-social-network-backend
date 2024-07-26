using API.Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace API.Blog.DAL
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<Entities.Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogId);

            modelBuilder.Entity<BlogTag>()
                .HasKey(bt => new { bt.BlogId, bt.TagId });

            modelBuilder.Entity<BlogTag>()
                .HasOne(bt => bt.Blog)
                .WithMany(b => b.BlogTags)
                .HasForeignKey(bt => bt.BlogId);

            modelBuilder.Entity<BlogTag>()
                .HasOne(bt => bt.Tag)
                .WithMany(t => t.BlogTags)
                .HasForeignKey(bt => bt.TagId);
        }
    }
}
