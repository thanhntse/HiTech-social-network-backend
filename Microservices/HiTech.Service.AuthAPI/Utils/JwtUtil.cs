using HiTech.Service.AuthAPI.DTOs.Message;
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

        public JwtUtil(IRefeshTokenRepository refreshTokenRepository, IConfiguration configuration)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _configuration = configuration;
        }

        public string GenerateJwtToken(LoginResponseMessage accountData)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, accountData.AccountId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, accountData.Email),
                new Claim(ClaimTypes.Role, accountData.Role),
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

        public async void SaveRefreshToken(int id, string refreshToken)
        {
            var token = new RefreshToken
            {
                Token = refreshToken,
                Expiry = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                AccountId = id,
            };
            await _refreshTokenRepository.CreateAsync(token);
        }
    }
}
