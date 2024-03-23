using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessObjects.InheritanceClass
{
    public class ProvideToken
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private static ProvideToken _instance;
        public static ProvideToken Instance => _instance;
        private ProvideToken(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }
        public static void Initialize(IConfiguration configuration, IMemoryCache memoryCache)
        {
            if (_instance == null)
                _instance = new ProvideToken(configuration, memoryCache);
        }

        public virtual string GenerateToken(int accountId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["AppSettings:SecretKey"];
            if (secretKey == null)
            {
                return "Not Found SecretKey";
            }
            var key = Encoding.ASCII.GetBytes(secretKey);
            // Tiếp tục với việc tạo token bằng key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            // Thêm các claim cần thiết vào đây (ví dụ: UserId)
            new Claim("AccountId", accountId.ToString()),
            new Claim(ClaimTypes.Role, role),
                }),
                Expires = DateTime.UtcNow.AddMinutes(10), // Thời gian hiệu lực của token (vd: 30 phút)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Lưu trữ token trong bộ nhớ
            _memoryCache.Set(accountId.ToString(), tokenString, TimeSpan.FromMinutes(10));

            return tokenString;
        }
    }
}   
