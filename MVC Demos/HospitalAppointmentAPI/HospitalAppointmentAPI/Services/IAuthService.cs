using System.Threading.Tasks;
using HospitalAppointmentAPI.DTOs;

namespace HospitalAppointmentAPI.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto dto);
    }
}
