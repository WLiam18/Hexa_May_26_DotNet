using Hospital.AuthService.DTOs;

namespace Hospital.AuthService.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}
