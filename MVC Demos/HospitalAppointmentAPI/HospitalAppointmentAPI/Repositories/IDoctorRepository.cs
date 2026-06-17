using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAppointmentAPI.Models;

namespace HospitalAppointmentAPI.Repositories
{
    public interface IDoctorRepository
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
