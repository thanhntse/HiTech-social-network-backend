using AutoMapper;
using HiTech.Service.AuthAPI.DTOs.Request;
using HiTech.Service.AuthAPI.DTOs.Response;
using HiTech.Service.AuthAPI.Entities;
using HiTech.Service.AuthAPI.Services.IService;
using HiTech.Service.AuthAPI.UOW;
using HiTech.Service.AuthAPI.Utils;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace HiTech.Service.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtUtil _jwtUtil;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUnitOfWork unitOfWork, JwtUtil jwtUtil, IMapper mapper, ILogger<AuthService> logger)
        {
            _unitOfWork = unitOfWork;
            _jwtUtil = jwtUtil;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AuthResponse?> Login(LoginRequest login)
        {
            var account = await _unitOfWork.Accounts.GetByEmailAsync(login.Email);
            if (account == null || account.IsDeleted == true || !PasswordEncoder.Verify(login.Password, account.Password))
            {
                return null;
            }

            var jwtToken = _jwtUtil.GenerateJwtToken(account);
            var refreshToken = _jwtUtil.GenerateRefreshToken();
            bool success = await _jwtUtil.SaveRefreshToken(account.AccountId, refreshToken);

            if (!success)
            {
                return null;
            }

            return new AuthResponse { AccessToken = jwtToken, RefreshToken = refreshToken };
        }

        public async Task<AuthResponse?> RefreshToken(string refreshToken)
        {
            var token = await _unitOfWork.RefeshTokens.GetByRefreshTokenAsync(refreshToken);

            if (token == null || !token.IsActive)
                return null;

            var newJwtToken = _jwtUtil.GenerateJwtToken(token.Account);

            var newRefreshToken = _jwtUtil.GenerateRefreshToken();
            bool success = await _jwtUtil.SaveRefreshToken(token.Account.AccountId, newRefreshToken);

            if (!success)
            {
                return null;
            }

            token.Revoked = DateTime.Now;  // Vô hiệu hóa refresh token cũ
            try
            {
                _unitOfWork.RefeshTokens.Update(token);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while disable the old refresh token at {Time}.", DateTime.Now);
                return null;
            }

            return new AuthResponse { AccessToken = newJwtToken, RefreshToken = newRefreshToken };
        }

        public async Task<bool> Logout(string id, LogoutRequest request)
        {
            try
            {
                var accountId = _jwtUtil.GetAccountIdFromToken(request.AccessToken);
                if (accountId == null || !accountId.Equals(id))
                    return false;

                var refeshToken = await _unitOfWork.RefeshTokens.GetByRefreshTokenAsync(request.RefreshToken);
                if (refeshToken == null || !refeshToken.IsActive)
                    return false;

                // Vô hiệu hóa access token
                var token = new ExpiredToken
                {
                    Token = request.AccessToken,
                    InvalidationTime = DateTime.Now,
                    AccountId = Int32.Parse(id),
                };
                await _unitOfWork.ExpiredTokens.CreateAsync(token);

                // Vô hiệu hóa refresh token
                refeshToken.Revoked = DateTime.Now;

                _unitOfWork.RefeshTokens.Update(refeshToken);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logout at {Time}.", DateTime.Now);
                return false;
            }

            return true;
        }

        public async Task<bool> IsTokenRevoked(string token)
        {
            var revokedToken = await _unitOfWork.ExpiredTokens.GetByIDAsync(token);
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var expiration = jwtToken.ValidTo;
            return expiration < DateTime.UtcNow || revokedToken != null;
        }

        public async Task<AccountResponse?> GetProfile(string id)
        {
            var account = await _unitOfWork.Accounts.GetByIDAsync(Int32.Parse(id));

            if (account == null)
            {
                return null;
            }

            return _mapper.Map<AccountResponse>(account);
        }
    }
}
