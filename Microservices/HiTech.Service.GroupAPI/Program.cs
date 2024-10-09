using HiTech.RabbitMQ.Consumer;
using HiTech.RabbitMQ.Publisher;
using HiTech.Service.GroupAPI.Data;
using HiTech.Service.GroupAPI.Extensions;
using HiTech.Service.GroupAPI.Mapper;
using HiTech.Service.GroupAPI.Middlewares;
using HiTech.Service.GroupAPI.Services;
using HiTech.Service.GroupAPI.Services.BackgroundServices;
using HiTech.Service.GroupAPI.Services.IService;
using HiTech.Service.GroupAPI.UOW;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace HiTech.Service.GroupAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database Context Dependency Injection
            builder.Services.AddDbContext<GroupDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:DBDefault"]));

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

            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<IJoinRequestService, JoinRequestService>();

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
