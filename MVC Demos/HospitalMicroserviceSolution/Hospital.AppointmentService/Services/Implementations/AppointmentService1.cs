using Hospital.AppointmentService.DTOs;
using Hospital.AppointmentService.Models;
using Hospital.AppointmentService.Repositories.Interfaces;
using Hospital.AppointmentService.Services.Interfaces;

namespace Hospital.AppointmentService.Services.Implementations
{
    public class AppointmentService1 : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IDoctorApiClient _doctorApiClient;
        private readonly IPatientApiClient _patientApiClient;

        public AppointmentService1(
            IAppointmentRepository appointmentRepository,
            IDoctorApiClient doctorApiClient,
            IPatientApiClient patientApiClient)
        {
            _appointmentRepository = appointmentRepository;
            _doctorApiClient = doctorApiClient;
            _patientApiClient = patientApiClient;
        }

        public async Task<List<AppointmentResponseDto>> GetAllAsync()
        {
            List<Appointment> appointments = await _appointmentRepository.GetAllAsync();

            List<AppointmentResponseDto> result = new List<AppointmentResponseDto>();

            foreach (Appointment appointment in appointments)
            {
                AppointmentResponseDto dto = await MapToResponseDtoAsync(appointment);
                result.Add(dto);
            }

            return result;
        }

        public async Task<AppointmentResponseDto?> GetByIdAsync(int id)
        {
            Appointment? appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
            {
                return null;
            }

            return await MapToResponseDtoAsync(appointment);
        }

        public async Task<AppointmentResponseDto?> CreateAsync(AppointmentCreateDto dto)
        {
            DoctorDto? doctor = await _doctorApiClient.GetDoctorByIdAsync(dto.DoctorId);
            PatientDto? patient = await _patientApiClient.GetPatientByIdAsync(dto.PatientId);

            if (doctor == null || patient == null)
            {
                return null;
            }

            Appointment appointment = new Appointment
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,
                AppointmentStatus = "Scheduled",
                Reason = dto.Reason,
                CreatedAt = DateTime.Now
            };

            await _appointmentRepository.AddAsync(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return await MapToResponseDtoAsync(appointment);
        }

        public async Task<bool> UpdateStatusAsync(int id, AppointmentUpdateStatusDto dto)
        {
            Appointment? appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
            {
                return false;
            }

            appointment.AppointmentStatus = dto.AppointmentStatus;

            _appointmentRepository.Update(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Appointment? appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment == null)
            {
                return false;
            }

            _appointmentRepository.Delete(appointment);
            await _appointmentRepository.SaveChangesAsync();

            return true;
        }

        private async Task<AppointmentResponseDto> MapToResponseDtoAsync(Appointment appointment)
        {
            DoctorDto? doctor = await _doctorApiClient.GetDoctorByIdAsync(appointment.DoctorId);
            PatientDto? patient = await _patientApiClient.GetPatientByIdAsync(appointment.PatientId);

            return new AppointmentResponseDto
            {
                AppointmentId = appointment.AppointmentId,
                DoctorId = appointment.DoctorId,
                DoctorName = doctor?.FullName ?? "Doctor not found",
                PatientId = appointment.PatientId,
                PatientName = patient?.FullName ?? "Patient not found",
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                AppointmentStatus = appointment.AppointmentStatus,
                Reason = appointment.Reason,
                CreatedAt = appointment.CreatedAt
            };
        }
    }
}
