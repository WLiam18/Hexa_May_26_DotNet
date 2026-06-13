using Api_Pagination_Sorting_Demo.Data;
using Api_Pagination_Sorting_Demo.Models;
using Api_Pagination_Sorting_Demo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Pagination_Sorting_Demo.Repository.Implementations
{
    public class AuthRepository: IAuthRepository
    {
        private readonly HospitalAppointmentDbContext _context;

        public AuthRepository(HospitalAppointmentDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
