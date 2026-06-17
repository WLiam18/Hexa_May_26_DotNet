using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAppointmentAPI.Models;

namespace HospitalAppointmentAPI.Repositories
{
    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
