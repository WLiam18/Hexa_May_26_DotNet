using Api_Pagination_Sorting_Demo.Dtos;

namespace Api_Pagination_Sorting_Demo.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<(bool Success, string Message, PagedResponseDto<AppointmentResponseDto>? Data)>
            GetPagedAppointmentsAsync(AppointmentFilterRequestDto filter);

    }
}
