using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalAppointmentAPI.Data;
using HospitalAppointmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HospitalAppointmentDbContext _db;

        public UserRepository(HospitalAppointmentDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _db.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _db.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(int userId)
        {
            var roles = await _db.UserRoles.Include(ur => ur.Role).Where(ur => ur.UserId == userId).Select(ur => ur.Role.Name).ToListAsync();
            return roles;
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return false;
            // NOTE: For demo purposes we compare plaintext password to PasswordHash column.
            // In production, store proper password hashes and verify with a secure algorithm.
            return user.PasswordHash == password;
        }
    }
}
