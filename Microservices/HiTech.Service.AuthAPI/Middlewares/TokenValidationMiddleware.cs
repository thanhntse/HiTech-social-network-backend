using HiTech.Service.AuthAPI.Services.IService;
using System.Text.RegularExpressions;

namespace HiTech.Service.AuthAPI.Middlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly Dictionary<string, HashSet<string>> _publicRoutes
            = new Dictionary<string, HashSet<string>>
            {
                { "GET", new HashSet<string> { "^/api/hitech/auth/validate-token$" } },
                { "POST", new HashSet<string> { "^/api/hitech/auth/login$", "^/api/hitech/auth/refresh-token$",
                    "^/api/hitech/accounts$" } }
            };

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IAuthService authService)
        {
            var path = context.Request.Path;
            var method = context.Request.Method;

            if (!IsPublicRoute(method, path))
            {
                var token = context.Request.Headers.Authorization.ToString().Replace("Bearer ", "");

                if (!string.IsNullOrEmpty(token) && await authService.IsTokenRevoked(token))
                {
                    context.Response.Headers.WWWAuthenticate = "Bearer error=\"invalid_token\", error_description=\"The token has been revoked or expired\"";
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("The token has been revoked or expired.");
                    return;
                }
            }
            await _next(context);
        }

        private bool IsPublicRoute(string method, string path)
        {
            if (_publicRoutes.TryGetValue(method, out var routes))
            {
                return routes.Any(route => Regex.IsMatch(path, route));
            }
            return false;
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
