using Api_Pagination_Sorting_Demo.Dtos;
using Api_Pagination_Sorting_Demo.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Pagination_Sorting_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAppointments([FromQuery] AppointmentFilterRequestDto filter)
        {
            var result = await _appointmentService.GetPagedAppointmentsAsync(filter);
            if (!result.Success)
            {
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = result.Message
                });
            }   
            return Ok(result.Data);
        }
    }
}
