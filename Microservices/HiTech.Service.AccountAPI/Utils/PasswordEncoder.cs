using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace HiTech.Service.AccountAPI.Utils
{
    public static class PasswordEncoder
    {
        public static string Encode(string password)
        {
            // Tạo muối (salt)
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Băm mật khẩu
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Kết hợp muối và mật khẩu băm thành một chuỗi
            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }

        // Hàm xác thực mật khẩu với mật khẩu băm
        public static bool Verify(string password, string hashedPassword)
        {
            // Tách muối và mật khẩu băm từ chuỗi đã lưu trữ
            var parts = hashedPassword.Split('.');
            if (parts.Length != 2)
            {
                return false;
            }

            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            // Băm lại mật khẩu với muối đã lấy được
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // So sánh mật khẩu băm mới với mật khẩu băm đã lưu trữ
            return hashed == storedHash;
        }
    }
}
