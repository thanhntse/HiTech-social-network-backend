using AutoMapper;
using HiTech.Service.AuthAPI.DTOs.Request;
using HiTech.Service.AuthAPI.DTOs.Response;
using HiTech.Service.AuthAPI.Entities;
using HiTech.Service.AuthAPI.Repositories;
using HiTech.Service.AuthAPI.Services.IService;
using HiTech.Service.AuthAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace HiTech.Service.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtUtil _jwtUtil;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IRefeshTokenRepository _refeshTokenRepository;
        private readonly IExpiredTokenRepository _expiredTokenRepository;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IAccountRepository accountRepository, IRefeshTokenRepository refeshTokenRepository,
            IExpiredTokenRepository expiredTokenRepository, JwtUtil jwtUtil, IMapper mapper, ILogger<AuthService> logger)
        {
            _accountRepository = accountRepository;
            _expiredTokenRepository = expiredTokenRepository;
            _refeshTokenRepository = refeshTokenRepository;
            _jwtUtil = jwtUtil;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AuthResponse?> Login(LoginRequest login)
        {
            var account = await _accountRepository.GetByEmailAsync(login.Email);
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
            var token = await _refeshTokenRepository.GetByRefreshTokenAsync(refreshToken);

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
                await _refeshTokenRepository.UpdateAsync(token);
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

                var refeshToken = await _refeshTokenRepository.GetByRefreshTokenAsync(request.RefreshToken);
                if (refeshToken == null || !refeshToken.IsActive)
                    return false;

                // Vô hiệu hóa access token
                var token = new ExpiredToken
                {
                    Token = request.AccessToken,
                    InvalidationTime = DateTime.Now,
                    AccountId = Int32.Parse(id),
                };
                await _expiredTokenRepository.CreateAsync(token);

                // Vô hiệu hóa refresh token
                refeshToken.Revoked = DateTime.Now;

                await _refeshTokenRepository.UpdateAsync(refeshToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logout at {Time}.", DateTime.Now);
                return false;
            }

            return true;
        }

        public async Task<AccountResponse?> GetProfile(string id)
        {
            var account = await _accountRepository.GetByIDAsync(Int32.Parse(id));

            if (account == null)
            {
                return null;
            }

            return _mapper.Map<AccountResponse>(account);
        }
    }
}
