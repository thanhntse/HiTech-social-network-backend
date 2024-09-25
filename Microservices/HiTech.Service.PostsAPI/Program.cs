using HiTech.Service.PostsAPI.Data;
using HiTech.Service.PostsAPI.Extensions;
using HiTech.Service.PostsAPI.Mapper;
using HiTech.Service.PostsAPI.Middlewares;
using HiTech.Service.PostsAPI.Services;
using HiTech.Service.PostsAPI.Services.IService;
using HiTech.Service.PostsAPI.UOW;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace HiTech.Service.PostsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Database Context Dependency Injection
            builder.Services.AddDbContext<PostDbContext>(opt => opt.UseSqlServer(builder.Configuration["ConnectionStrings:DBDefault"]));

            //Add custom JWT Authentication
            builder.AddHiTechAuthetication();
            builder.Services.AddAuthorization();

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IPostService, PostService>();

            builder.Services.AddControllers();

            builder.Services.AddHttpClient("HiTech", client =>
            {
                client.BaseAddress = new Uri("http://localhost:8001/api/hitech/");
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
