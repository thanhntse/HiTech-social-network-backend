using HiTech.Service.AuthAPI.DTOs.Request;
using HiTech.Service.AuthAPI.DTOs.Response;
using HiTech.Service.AuthAPI.Entities;
using HiTech.Service.AuthAPI.Repositories;
using HiTech.Service.AuthAPI.Utils;

namespace HiTech.Service.AuthAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtUtil _jwtUtil;
        private readonly IRefeshTokenRepository _refeshTokenRepository;
        private readonly IExpiredTokenRepository _expiredTokenRepository;

        public AuthService(IRefeshTokenRepository refeshTokenRepository, 
            IExpiredTokenRepository expiredTokenRepository, JwtUtil jwtUtil)
        {
            _expiredTokenRepository = expiredTokenRepository;
            _refeshTokenRepository = refeshTokenRepository;
            _jwtUtil = jwtUtil;
        }

        public async Task<AuthResponse?> Login(LoginRequest login)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(a => a.Username == login.Username);
            if (account == null || account.IsDeleted == true || !PasswordEncoder.Verify(account.Password, login.Password))
            {
                return null;
            }

            var jwtToken = _jwtUtil.GenerateJwtToken(account);
            var refreshToken = _jwtUtil.GenerateRefreshToken();
            _jwtUtil.SaveRefreshToken(account, refreshToken);

            return new AuthResponse { AccessToken = jwtToken, RefreshToken = refreshToken };
        }

        public async Task<AuthResponse?> RefreshToken(string refreshToken)
        {
            var token = await _context.RefreshTokens
                .Include(s => s.Account)
                .FirstOrDefaultAsync(x => x.Token == refreshToken);

            if (token == null || !token.IsActive)
                return null;

            var newJwtToken = _jwtUtil.GenerateJwtToken(token.Account);

            var newRefreshToken = _jwtUtil.GenerateRefreshToken();
            token.Revoked = DateTime.Now;  // Vô hiệu hóa refresh token cũ
            _jwtUtil.SaveRefreshToken(token.Account, newRefreshToken);

            return new AuthResponse { AccessToken = newJwtToken, RefreshToken = newRefreshToken };
        }

        public async Task<bool> Logout(string id, LogoutRequest request)
        {
            if (request.AccessToken == null)
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

            return true;
        }

        //public async Task<AccountResponse?> GetProfile(string id)
        //{
        //    var account = await _context.Accounts.FindAsync(Int32.Parse(id));

        //    if (account == null)
        //    {
        //        return null;
        //    }

        //    return _mapper.Map<AccountResponse>(account);
        //}
    }
}
