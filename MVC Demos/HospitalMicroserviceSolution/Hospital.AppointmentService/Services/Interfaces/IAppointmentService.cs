using Hospital.AppointmentService.DTOs;

namespace Hospital.AppointmentService.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<AppointmentResponseDto>> GetAllAsync();

        Task<AppointmentResponseDto?> GetByIdAsync(int id);

        Task<AppointmentResponseDto?> CreateAsync(AppointmentCreateDto dto);

        Task<bool> UpdateStatusAsync(int id, AppointmentUpdateStatusDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
