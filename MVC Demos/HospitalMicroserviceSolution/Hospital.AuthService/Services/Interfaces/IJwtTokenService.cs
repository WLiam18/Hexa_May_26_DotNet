using Hospital.AuthService.Models;

namespace Hospital.AuthService.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user, List<string> roles);
    }
}
