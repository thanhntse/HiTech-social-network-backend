using API.User.DAL;
using API.User.Mapper;
using API.User.Repositories;
using API.User.Services;
using Microsoft.EntityFrameworkCore;

namespace API.User
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
            builder.Services.AddDbContext<UserDbContext>(opt => opt.UseSqlServer(GetConnectionString()));

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
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
