using Api_Pagination_Sorting_Demo.Dtos;

namespace Api_Pagination_Sorting_Demo.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, LoginResponseDto? Data)> LoginAsync(
            LoginRequestDto loginRequestDto);
    }
}
