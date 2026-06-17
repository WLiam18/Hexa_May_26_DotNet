using HospitalAppointmentMvc.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalAppointmentMvc.services.Interfaces
{
    public interface IAppointmentApiService
    {
        Task<List<AppointmentResponseDto>> GetAllAppointmentsAsync();

        Task<AppointmentResponseDto?> GetAppointmentByIdAsync(int id);

        Task<bool> CreateAppointmentAsync(AppointmentCreateDto appointment);

        Task<bool> UpdateAppointmentStatusAsync(int id, AppointmentUpdateStatusDto statusDto);

        Task<bool> DeleteAppointmentAsync(int id);

        Task<SelectList> GetDoctorsSelectListAsync(int? selectedDoctorId = null);

        Task<SelectList> GetPatientsSelectListAsync(int? selectedPatientId = null);
    }
}
