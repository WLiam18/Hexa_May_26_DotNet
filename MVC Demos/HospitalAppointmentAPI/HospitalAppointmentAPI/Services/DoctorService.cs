using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalAppointmentAPI.DTOs;
using HospitalAppointmentAPI.Repositories;

namespace HospitalAppointmentAPI.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepo;

        public DoctorService(IDoctorRepository doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        public async Task<IEnumerable<DoctorResponseDto>> GetAllAsync()
        {
            var doctors = await _doctorRepo.GetAllAsync();
            return doctors.Select(d => new DoctorResponseDto
            {
                DoctorId = d.DoctorId,
                FullName = d.FullName,
                Specialization = d.Specialization,
                Email = d.Email,
                PhoneNumber = d.PhoneNumber,
                ConsultationFee = d.ConsultationFee,
                IsAvailable = d.IsAvailable
            });
        }

        public async Task<DoctorResponseDto?> GetByIdAsync(int id)
        {
            var d = await _doctorRepo.GetByIdAsync(id);
            if (d == null) return null;
            return new DoctorResponseDto
            {
                DoctorId = d.DoctorId,
                FullName = d.FullName,
                Specialization = d.Specialization,
                Email = d.Email,
                PhoneNumber = d.PhoneNumber,
                ConsultationFee = d.ConsultationFee,
                IsAvailable = d.IsAvailable
            };
        }
    }
}
