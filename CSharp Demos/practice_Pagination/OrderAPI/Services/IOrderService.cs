using System.Threading.Tasks;
using OrderAPI.DTOs;

namespace OrderAPI.Services
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateOrderAsync(OrderCreateDto dto);
        Task<PagedResponseDto<OrderResponseDto>> GetOrdersAsync(OrderFilterDto filter);
    }
}