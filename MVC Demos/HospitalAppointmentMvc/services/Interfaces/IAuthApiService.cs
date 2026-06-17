using HospitalAppointmentMvc.Dtos;

namespace HospitalAppointmentMvc.services.Interfaces
{
    public interface IAuthApiService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequest);
    }
}
