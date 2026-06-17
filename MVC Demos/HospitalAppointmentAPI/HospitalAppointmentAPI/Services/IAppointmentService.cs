using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAppointmentAPI.DTOs;

namespace HospitalAppointmentAPI.Services
{
    public interface IAppointmentService
    {
        Task<IEnumerable<AppointmentResponseDto>> GetAllAsync();
        Task<AppointmentResponseDto?> GetByIdAsync(int id);
        Task<AppointmentResponseDto> CreateAsync(AppointmentCreateDto dto);
        Task UpdateStatusAsync(int id, AppointmentUpdateStatusDto dto);
        Task DeleteAsync(int id);
    }
}
