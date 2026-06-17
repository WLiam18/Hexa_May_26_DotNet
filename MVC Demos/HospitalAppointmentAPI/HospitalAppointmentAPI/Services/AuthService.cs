using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HospitalAppointmentAPI.DTOs;
using HospitalAppointmentAPI.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HospitalAppointmentAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;
        private readonly IConfiguration _config;

        public AuthService(IUserRepository userRepo, IConfiguration config)
        {
            _userRepo = userRepo;
            _config = config;
        }

        public async Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto dto)
        {
            var valid = await _userRepo.ValidateCredentialsAsync(dto.Username, dto.Password);
            if (!valid) return new LoginResponseDto { Success = false, Token = null };

            var user = await _userRepo.GetByUsernameAsync(dto.Username);
            var roles = user?.UserRoles.Select(ur => ur.Role.Name).ToList() ?? new List<string>();

            var token = GenerateToken(user!, roles);
            return new LoginResponseDto { Success = true, Token = token };
        }

        private string GenerateToken(Models.User user, List<string> roles)
        {
            var jwtSection = _config.GetSection("JwtSettings");
            var secret = jwtSection.GetValue<string>("Secret") ?? throw new InvalidOperationException("JWT Secret not configured");
            var issuer = jwtSection.GetValue<string>("Issuer");
            var audience = jwtSection.GetValue<string>("Audience");
            var expiryMinutes = jwtSection.GetValue<int?>("ExpiryMinutes") ?? 60;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("fullname", user.FullName)
            };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
