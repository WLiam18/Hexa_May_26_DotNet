using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalAppointmentAPI.DTOs;
using HospitalAppointmentAPI.Repositories;

namespace HospitalAppointmentAPI.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepo;

        public PatientService(IPatientRepository patientRepo)
        {
            _patientRepo = patientRepo;
        }

        public async Task<IEnumerable<PatientResponseDto>> GetAllAsync()
        {
            var patients = await _patientRepo.GetAllAsync();
            return patients.Select(p => new PatientResponseDto
            {
                PatientId = p.PatientId,
                FullName = p.FullName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                Address = p.Address
            });
        }

        public async Task<PatientResponseDto?> GetByIdAsync(int id)
        {
            var p = await _patientRepo.GetByIdAsync(id);
            if (p == null) return null;
            return new PatientResponseDto
            {
                PatientId = p.PatientId,
                FullName = p.FullName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                Address = p.Address
            };
        }
    }
}
