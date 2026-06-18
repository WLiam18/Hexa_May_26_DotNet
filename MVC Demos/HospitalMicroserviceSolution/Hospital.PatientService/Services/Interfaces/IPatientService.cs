using Hospital.PatientService.DTOs;

namespace Hospital.PatientService.Services.Interfaces
{
    public interface IPatientService
    {
        Task<List<PatientResponseDto>> GetAllAsync();

        Task<PatientResponseDto?> GetByIdAsync(int id);

        Task<PatientResponseDto> CreateAsync(PatientCreateDto dto);

        Task<bool> UpdateAsync(int id, PatientCreateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
