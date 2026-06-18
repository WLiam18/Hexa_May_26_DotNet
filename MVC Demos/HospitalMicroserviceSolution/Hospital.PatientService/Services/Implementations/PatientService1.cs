using Hospital.PatientService.DTOs;
using Hospital.PatientService.Models;
using Hospital.PatientService.Repositories.Interfaces;
using Hospital.PatientService.Services.Interfaces;

namespace Hospital.PatientService.Services.Implementations
{
    public class PatientService1 : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService1(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<List<PatientResponseDto>> GetAllAsync()
        {
            List<Patient> patients = await _patientRepository.GetAllAsync();

            return patients.Select(MapToResponseDto).ToList();
        }

        public async Task<PatientResponseDto?> GetByIdAsync(int id)
        {
            Patient? patient = await _patientRepository.GetByIdAsync(id);

            if (patient == null)
            {
                return null;
            }

            return MapToResponseDto(patient);
        }

        public async Task<PatientResponseDto> CreateAsync(PatientCreateDto dto)
        {
            Patient patient = new Patient
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Address = dto.Address
            };

            await _patientRepository.AddAsync(patient);
            await _patientRepository.SaveChangesAsync();

            return MapToResponseDto(patient);
        }

        public async Task<bool> UpdateAsync(int id, PatientCreateDto dto)
        {
            Patient? patient = await _patientRepository.GetByIdAsync(id);

            if (patient == null)
            {
                return false;
            }

            patient.FullName = dto.FullName;
            patient.Email = dto.Email;
            patient.PhoneNumber = dto.PhoneNumber;
            patient.DateOfBirth = dto.DateOfBirth;
            patient.Gender = dto.Gender;
            patient.Address = dto.Address;

            _patientRepository.Update(patient);
            await _patientRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Patient? patient = await _patientRepository.GetByIdAsync(id);

            if (patient == null)
            {
                return false;
            }

            _patientRepository.Delete(patient);
            await _patientRepository.SaveChangesAsync();

            return true;
        }

        private static PatientResponseDto MapToResponseDto(Patient patient)
        {
            return new PatientResponseDto
            {
                PatientId = patient.PatientId,
                FullName = patient.FullName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                Address = patient.Address
            };
        }
    
}
}
