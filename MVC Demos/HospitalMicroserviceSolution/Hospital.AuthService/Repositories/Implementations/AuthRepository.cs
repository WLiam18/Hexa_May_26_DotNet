using Hospital.AuthService.Data;
using Hospital.AuthService.Models;
using Hospital.AuthService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hospital.AuthService.Repositories.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthDbContext _context;

        public AuthRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);
        }
    }
}
