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
            var authHeader = context.Request.Headers.Authorization.ToString();

            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();

                HttpClient client = _factory.CreateClient("HiTech");
                var response = await client.GetAsync($"auth/validate-token?token={token}");
                if (!response.IsSuccessStatusCode)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token has revoked.");
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
