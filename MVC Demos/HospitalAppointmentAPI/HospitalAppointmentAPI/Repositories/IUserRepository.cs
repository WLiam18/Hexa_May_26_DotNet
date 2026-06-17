using System.Threading.Tasks;
using HospitalAppointmentAPI.Models;
using System.Collections.Generic;

namespace HospitalAppointmentAPI.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<string>> GetRolesAsync(int userId);
        Task<bool> ValidateCredentialsAsync(string username, string password);
    }
}
