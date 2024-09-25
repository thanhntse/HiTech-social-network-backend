using HiTech.Service.AuthAPI.DTOs.Request;
using HiTech.Service.AuthAPI.DTOs.Response;

namespace HiTech.Service.AuthAPI.Services.IService
{
    public interface IAuthService
    {
        Task<AuthResponse?> Login(LoginRequest login);
        Task<AuthResponse?> RefreshToken(string refreshToken);
        Task<bool> Logout(string id, LogoutRequest request);
        Task<bool> IsValidToken(string token);
        Task<AccountResponse?> GetProfile(string id);
    }
}
