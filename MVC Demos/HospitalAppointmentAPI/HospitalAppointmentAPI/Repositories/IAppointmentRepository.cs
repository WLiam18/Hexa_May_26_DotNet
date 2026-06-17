using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAppointmentAPI.Models;

namespace HospitalAppointmentAPI.Repositories
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int id);
        Task<Appointment> CreateAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Appointment appointment);
        Task<bool> ExistsAsync(int id);
    }
}
