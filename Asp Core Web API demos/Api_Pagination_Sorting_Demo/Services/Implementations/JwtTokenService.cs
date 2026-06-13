using Api_Pagination_Sorting_Demo.Models;
using Api_Pagination_Sorting_Demo.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Api_Pagination_Sorting_Demo.Services.Implementations
{
    public class JwtTokenService: IJwtTokenService
    { private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user, List<string> roles, out DateTime expiresAt)
        {
            string issuer = _configuration["JwtSettings:Issuer"]!;
            string audience = _configuration["JwtSettings:Audience"]!;
            string secretKey = _configuration["JwtSettings:SecretKey"]!;
            int expiryMinutes = int.Parse(_configuration["JwtSettings:ExpiryMinutes"]!);

            expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("FullName", user.FullName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
                                                                                                      
        }   
}


