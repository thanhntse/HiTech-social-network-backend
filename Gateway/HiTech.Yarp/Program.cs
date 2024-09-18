namespace HiTech.Yarp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
                .AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.MapReverseProxy();

            app.Run();
        }
    }
}
