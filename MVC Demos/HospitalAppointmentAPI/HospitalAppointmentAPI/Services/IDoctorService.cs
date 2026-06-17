using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAppointmentAPI.DTOs;

namespace HospitalAppointmentAPI.Services
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponseDto>> GetAllAsync();
        Task<DoctorResponseDto?> GetByIdAsync(int id);
    }
}
