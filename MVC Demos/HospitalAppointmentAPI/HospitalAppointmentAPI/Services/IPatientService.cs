using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAppointmentAPI.DTOs;

namespace HospitalAppointmentAPI.Services
{
    public interface IPatientService
    {
        Task<IEnumerable<PatientResponseDto>> GetAllAsync();
        Task<PatientResponseDto?> GetByIdAsync(int id);
    }
}
