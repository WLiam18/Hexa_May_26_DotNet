using Hospital.AuthService.DTOs;
using Hospital.AuthService.Models;
using Hospital.AuthService.Repositories.Interfaces;
using Hospital.AuthService.Services.Interfaces;

namespace Hospital.AuthService.Services.Implementations
{
    public class AuthService1 : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService1(
            IAuthRepository authRepository,
            IJwtTokenService jwtTokenService)
        {
            _authRepository = authRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            User? user = await _authRepository.GetUserByUsernameAsync(request.Username);

            if (user == null)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                user.PasswordHash);

            if (!isPasswordValid)
            {
                return new LoginResponseDto
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            List<string> roles = user.UserRoles
                .Where(ur => ur.Role != null)
                .Select(ur => ur.Role!.RoleName)
                .ToList();

            string token = _jwtTokenService.GenerateToken(user, roles);

            return new LoginResponseDto
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                Username = user.Username,
                FullName = user.FullName,
                Roles = roles
            };
        }
    }
}
