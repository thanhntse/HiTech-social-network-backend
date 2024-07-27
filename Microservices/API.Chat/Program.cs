using API.Chat.SignalR;

namespace API.Chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSignalR();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000")
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.



            app.UseRouting();

            app.UseAuthorization();
            app.UseCors();

            app.MapControllers();
            app.MapHub<ChatHub>("/chathub");

            app.Run();
        }
    }
}
