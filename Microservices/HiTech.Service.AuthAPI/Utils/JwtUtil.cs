using HiTech.Service.AuthAPI.Entities;
using HiTech.Service.AuthAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HiTech.Service.AuthAPI.Utils
{
    public class JwtUtil
    {
        private readonly IRefeshTokenRepository _refreshTokenRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<JwtUtil> _logger;

        public JwtUtil(IRefeshTokenRepository refreshTokenRepository, IConfiguration configuration, ILogger<JwtUtil> logger)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
            _logger = logger;
        }

        public string GenerateJwtToken(Account account)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, account.AccountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, account.Email),
                new Claim(ClaimTypes.Role, account.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }

        public async Task<bool> SaveRefreshToken(int id, string refreshToken)
        {
            var token = new RefreshToken
            {
                Token = refreshToken,
                Expiry = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                AccountId = id,
            };

            try
            {
                await _refreshTokenRepository.CreateAsync(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the refresh token at {Time}.", DateTime.Now);
                return false;
            }
            return true;
        }

        public string? GetAccountIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            // Kiểm tra xem token có hợp lệ không
            if (handler.CanReadToken(token))
            {
                var jwtToken = handler.ReadJwtToken(token);

                // Lấy Claim chứa AccountId
                var accountIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub);

                if (accountIdClaim != null)
                {
                    return accountIdClaim.Value; // Trả về AccountId
                }
            }

            return null; // Hoặc xử lý trường hợp không tìm thấy token hợp lệ
        }
    }
}
