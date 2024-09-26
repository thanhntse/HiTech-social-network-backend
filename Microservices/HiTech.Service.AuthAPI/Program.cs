using HiTech.Service.AuthAPI.Data;
using HiTech.Service.AuthAPI.Mapper;
using HiTech.Service.AuthAPI.Services.IService;
using HiTech.Service.AuthAPI.Services;
using Microsoft.EntityFrameworkCore;
using HiTech.Service.AuthAPI.Utils;
using HiTech.Service.AuthAPI.UOW;
using HiTech.Service.AuthAPI.Extensions;
using HiTech.Service.AuthAPI.Middlewares;
using HiTech.RabbitMQ.Publisher;

namespace HiTech.Service.AuthAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database Context Dependency Injection
            builder.Services.AddDbContext<AuthDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:DBDefault"]));

            //Add custom JWT Authentication
            builder.AddHiTechAuthetication();
            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddScoped<JwtUtil>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // Custom swagger
            builder.AddHiTechSwagger();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            // Custom middleware check token revoked
            app.UseTokenValidation();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
