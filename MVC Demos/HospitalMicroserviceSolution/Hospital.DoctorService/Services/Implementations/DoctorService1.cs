using Hospital.DoctorService.DTOs;
using Hospital.DoctorService.Models;
using Hospital.DoctorService.Repositories.Interfaces;
using Hospital.DoctorService.Services.Interfaces;

namespace Hospital.DoctorService.Services.Implementations
{
    public class DoctorService1 : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService1(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public async Task<List<DoctorResponseDto>> GetAllAsync()
        {
            List<Doctor> doctors = await _doctorRepository.GetAllAsync();

            return doctors.Select(MapToResponseDto).ToList();
        }

        public async Task<DoctorResponseDto?> GetByIdAsync(int id)
        {
            Doctor? doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
            {
                return null;
            }

            return MapToResponseDto(doctor);
        }

        public async Task<DoctorResponseDto> CreateAsync(DoctorCreateDto dto)
        {
            Doctor doctor = new Doctor
            {
                FullName = dto.FullName,
                Specialization = dto.Specialization,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                ConsultationFee = dto.ConsultationFee,
                IsAvailable = dto.IsAvailable
            };

            await _doctorRepository.AddAsync(doctor);
            await _doctorRepository.SaveChangesAsync();

            return MapToResponseDto(doctor);
        }

        public async Task<bool> UpdateAsync(int id, DoctorCreateDto dto)
        {
            Doctor? doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
            {
                return false;
            }

            doctor.FullName = dto.FullName;
            doctor.Specialization = dto.Specialization;
            doctor.Email = dto.Email;
            doctor.PhoneNumber = dto.PhoneNumber;
            doctor.ConsultationFee = dto.ConsultationFee;
            doctor.IsAvailable = dto.IsAvailable;

            _doctorRepository.Update(doctor);
            await _doctorRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Doctor? doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor == null)
            {
                return false;
            }

            _doctorRepository.Delete(doctor);
            await _doctorRepository.SaveChangesAsync();

            return true;
        }

        private static DoctorResponseDto MapToResponseDto(Doctor doctor)
        {
            return new DoctorResponseDto
            {
                DoctorId = doctor.DoctorId,
                FullName = doctor.FullName,
                Specialization = doctor.Specialization,
                Email = doctor.Email,
                PhoneNumber = doctor.PhoneNumber,
                ConsultationFee = doctor.ConsultationFee,
                IsAvailable = doctor.IsAvailable
            };
        }
    }
}
