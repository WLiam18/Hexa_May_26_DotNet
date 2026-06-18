using Hospital.DoctorService.DTOs;

namespace Hospital.DoctorService.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<List<DoctorResponseDto>> GetAllAsync();

        Task<DoctorResponseDto?> GetByIdAsync(int id);

        Task<DoctorResponseDto> CreateAsync(DoctorCreateDto dto);

        Task<bool> UpdateAsync(int id, DoctorCreateDto dto);

        Task<bool> DeleteAsync(int id);
    }
}
