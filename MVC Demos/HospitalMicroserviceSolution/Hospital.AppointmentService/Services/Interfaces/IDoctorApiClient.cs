using Hospital.AppointmentService.DTOs;

namespace Hospital.AppointmentService.Services.Interfaces
{
    public interface IDoctorApiClient
    {
        Task<DoctorDto?> GetDoctorByIdAsync(int doctorId);
    }
}
