using System.Security.Claims;
using System.Text.Json;

namespace HiTech.Service.PostsAPI.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _factory;

        public TokenValidationMiddleware(RequestDelegate next, IHttpClientFactory factory)
        {
            _next = next;
            _factory = factory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                HttpClient client = _factory.CreateClient("HiTech");
                var response = await client.GetAsync($"auth/validate-token?token={token}");

                if (!response.IsSuccessStatusCode)
                {
                    context.Response.Headers.WWWAuthenticate = "Bearer error=\"invalid_token\", error_description=\"The token has been revoked or expired\"";
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("The token has been revoked or expired.");
                    return;
                }
            }
            await _next(context);
        }
    }

    public static class TokenValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseTokenValidation(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<TokenValidationMiddleware>();
        }
    }
}
