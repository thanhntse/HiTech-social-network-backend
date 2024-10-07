using HiTech.RabbitMQ.Consumer;
using HiTech.RabbitMQ.Publisher;
using HiTech.Service.FriendAPI.Data;
using HiTech.Service.FriendAPI.Extensions;
using HiTech.Service.FriendAPI.Mapper;
using HiTech.Service.FriendAPI.Middlewares;
using HiTech.Service.FriendAPI.Services;
using HiTech.Service.FriendAPI.Services.BackgroundServices;
using HiTech.Service.FriendAPI.Services.IService;
using HiTech.Service.FriendAPI.UOW;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace HiTech.Service.FriendAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database Context Dependency Injection
            builder.Services.AddDbContext<FriendDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:DBDefault"]));

            //Add custom JWT Authentication
            builder.AddHiTechAuthetication();
            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddSingleton<IMessagePublisher, MessagePublisher>();
            builder.Services.AddSingleton<IMessageConsumer, MessageConsumer>();
            builder.Services.AddHostedService<UserCreateUpdateService>();
            builder.Services.AddHostedService<UserDeleteService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IFriendRequestService, FriendRequestService>();
            builder.Services.AddScoped<IFriendshipService, FriendshipService>();

            builder.Services.AddControllers();

            builder.Services.AddHttpClient("HiTech", client =>
            {
                client.BaseAddress = new Uri("http://host.docker.internal:8001/api/hitech/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
