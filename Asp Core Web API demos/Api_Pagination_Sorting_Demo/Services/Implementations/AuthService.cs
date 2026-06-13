using Api_Pagination_Sorting_Demo.Dtos;
using Api_Pagination_Sorting_Demo.Repository.Interfaces;
using Api_Pagination_Sorting_Demo.Services.Interfaces;

namespace Api_Pagination_Sorting_Demo.Services.Implementations
{
    public class AuthService: IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthService(IAuthRepository authRepository, IJwtTokenService jwtTokenService)
        {
            _authRepository = authRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<(bool Success,string Message,LoginResponseDto? Data)> LoginAsync(
            LoginRequestDto loginRequestDto)
        {
            if(string.IsNullOrWhiteSpace(loginRequestDto.UserName))
            {
                return (false, "Username is required.", null);
            }

            if (string.IsNullOrWhiteSpace(loginRequestDto.Password))
            {
                return (false, "Password is required.", null);
            }

            var user = await _authRepository.GetUserByUserNameAsync(loginRequestDto.UserName);
            if(user == null)
            {
                return (false, "Invalid username or password.", null);
            }
            if (!user.IsActive)
            {
                return (false, "User account us inactive", null);
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(
                loginRequestDto.Password, user.PasswordHash);

            if(!isPasswordValid)
            {
                return (false, "Invalid username and password", null);
            }

            var roles = user.UserRoles
                .Select(ur =>ur.Role.RoleName) .ToList();

            string token = _jwtTokenService.GenerateToken(user, roles, out DateTime expiresAt);
            var response = new LoginResponseDto
            {
                UserId = user.UserId,
                FullName = user.FullName,
                UserName = user.UserName,
                Roles = roles,
                Token = token,
                TokenExpiresAt = expiresAt
            };
            return (true, "Login Succesful", response);
        }
    }
}
