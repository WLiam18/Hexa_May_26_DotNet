using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalAppointmentAPI.DTOs;
using HospitalAppointmentAPI.Exceptions;
using HospitalAppointmentAPI.Models;
using HospitalAppointmentAPI.Repositories;

namespace HospitalAppointmentAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IDoctorRepository _doctorRepo;
        private readonly IPatientRepository _patientRepo;

        private static readonly string[] ValidStatuses = new[] { "Scheduled", "Completed", "Cancelled" };

        public AppointmentService(IAppointmentRepository appointmentRepo, IDoctorRepository doctorRepo, IPatientRepository patientRepo)
        {
            _appointmentRepo = appointmentRepo;
            _doctorRepo = doctorRepo;
            _patientRepo = patientRepo;
        }

        public async Task<IEnumerable<AppointmentResponseDto>> GetAllAsync()
        {
            var list = await _appointmentRepo.GetAllAsync();
            return list.Select(MapToDto);
        }

        public async Task<AppointmentResponseDto?> GetByIdAsync(int id)
        {
            var a = await _appointmentRepo.GetByIdAsync(id);
            if (a == null) return null;
            return MapToDto(a);
        }

        public async Task<AppointmentResponseDto> CreateAsync(AppointmentCreateDto dto)
        {
            if (!await _doctorRepo.ExistsAsync(dto.DoctorId))
                throw new NotFoundException($"Doctor with id {dto.DoctorId} not found.");

            if (!await _patientRepo.ExistsAsync(dto.PatientId))
                throw new NotFoundException($"Patient with id {dto.PatientId} not found.");

            var appointmentDateTime = dto.AppointmentDate.ToDateTime(dto.AppointmentTime);
            if (appointmentDateTime < DateTime.Now)
                throw new ValidationException("Appointment date and time cannot be in the past.");

            var appointment = new Appointment
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,
                Reason = dto.Reason,
                AppointmentStatus = "Scheduled",
                CreatedAt = DateTime.Now
            };

            var created = await _appointmentRepo.CreateAsync(appointment);
            return MapToDto(created);
        }

        public async Task UpdateStatusAsync(int id, AppointmentUpdateStatusDto dto)
        {
            if (!ValidStatuses.Contains(dto.AppointmentStatus))
                throw new ValidationException("Invalid appointment status.");

            var appointment = await _appointmentRepo.GetByIdAsync(id);
            if (appointment == null)
                throw new NotFoundException($"Appointment with id {id} not found.");

            appointment.AppointmentStatus = dto.AppointmentStatus;
            await _appointmentRepo.UpdateAsync(appointment);
        }

        public async Task DeleteAsync(int id)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(id);
            if (appointment == null)
                throw new NotFoundException($"Appointment with id {id} not found.");

            await _appointmentRepo.DeleteAsync(appointment);
        }

        private static AppointmentResponseDto MapToDto(Appointment a)
        {
            return new AppointmentResponseDto
            {
                AppointmentId = a.AppointmentId,
                DoctorId = a.DoctorId,
                DoctorName = a.Doctor?.FullName ?? string.Empty,
                PatientId = a.PatientId,
                PatientName = a.Patient?.FullName ?? string.Empty,
                AppointmentDate = a.AppointmentDate,
                AppointmentTime = a.AppointmentTime,
                AppointmentStatus = a.AppointmentStatus,
                Reason = a.Reason,
                CreatedAt = a.CreatedAt
            };
        }
    }
}
