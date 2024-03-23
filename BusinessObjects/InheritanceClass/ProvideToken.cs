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

        // Constructor with IConfiguration injection
        public ProvideToken(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        // Static method for initializing ProvideToken.Instance
        public static void Initialize(IConfiguration configuration)
        {
            if (Instance == null)
                Instance = new ProvideToken(configuration);
        }

        // Static property for accessing ProvideToken.Instance
        public static ProvideToken Instance { get; private set; }

        // Method for generating token
        public virtual string GenerateToken(int accountId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration["AppSettings:SecretKey"];
            if (secretKey == null)
            {
                return "Not Found SecretKey";
            }
            var key = Encoding.ASCII.GetBytes(secretKey);

            // Create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    // Add necessary claims here (e.g., UserId)
                    new Claim(ClaimTypes.UserData, accountId.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(60), // Token expiration time (e.g., 60 minutes)
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            // Create token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
