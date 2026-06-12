using Microsoft.AspNetCore.Mvc;
using OrderAPI.DTOs;
using OrderAPI.Services;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
        {
            try
            {
                var result = await _orderService.CreateOrderAsync(dto);
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Order created successfully",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderFilterDto filter)
        {
            var result = await _orderService.GetOrdersAsync(filter);
            return Ok(new
            {
                StatusCode = 200,
                Message = "Orders retrieved successfully",
                Data = result
            });
        }
    }
}