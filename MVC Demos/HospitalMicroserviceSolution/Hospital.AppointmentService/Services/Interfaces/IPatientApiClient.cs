using Hospital.AppointmentService.DTOs;

namespace Hospital.AppointmentService.Services.Interfaces
{
    public interface IPatientApiClient
    {
        Task<PatientDto?> GetPatientByIdAsync(int patientId);
    }
}
