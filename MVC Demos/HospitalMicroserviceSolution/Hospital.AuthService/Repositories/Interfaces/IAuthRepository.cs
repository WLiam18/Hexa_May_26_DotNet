using Hospital.AuthService.Models;

namespace Hospital.AuthService.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
    }
}
