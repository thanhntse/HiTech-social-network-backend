using HiTech.RabbitMQ.Consumer;
using HiTech.Service.NotificationAPI.Data;
using HiTech.Service.NotificationAPI.Extensions;
using HiTech.Service.NotificationAPI.Mapper;
using HiTech.Service.NotificationAPI.Middlewares;
using HiTech.Service.NotificationAPI.Services;
using HiTech.Service.NotificationAPI.Services.BackgroundServices;
using HiTech.Service.NotificationAPI.Services.IService;
using HiTech.Service.NotificationAPI.UOW;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace HiTech.Service.NotificationAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database Context Dependency Injection
            builder.Services.AddDbContext<NotificationDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:DBDefault"]));

            //Add custom JWT Authentication
            builder.AddHiTechAuthetication();
            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddSingleton<IMessageConsumer, MessageConsumer>();
            builder.Services.AddHostedService<NotificationCreateService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<INotificationService, NotificationService>();

            builder.Services.AddControllers();

            builder.Services.AddHttpClient("HiTech", client =>
            {
                client.BaseAddress = new Uri("https://host.docker.internal:9001/api/hitech/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });
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
            //Add custom middleware
            app.UseTokenValidation();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
