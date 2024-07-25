using BlogWebAPI.DAL;
using BlogWebAPI.Repositories;
using BlogWebAPI.Services;
using Microsoft.EntityFrameworkCore;
using UserWebAPI.Mapper;

namespace BlogWebAPI
{
    public class Program
    {
        private static string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true).Build();
            return configuration["ConnectionStrings:DBDefault"];
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database Context Dependency Injection
            builder.Services.AddDbContext<BlogDbContext>(opt => opt.UseSqlServer(GetConnectionString()));

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<IBlogService, BlogService>();

            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<ITagService, TagService>();

            builder.Services.AddScoped<ICommentRepository, CommentRepository>();
            builder.Services.AddScoped<ICommentService, CommentService>();

            builder.Services.AddControllers();


            var app = builder.Build();

            app.UsePathBase("/hitech");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
