using Api_Pagination_Sorting_Demo.Dtos;
using Api_Pagination_Sorting_Demo.Models;
using Api_Pagination_Sorting_Demo.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
namespace Api_Pagination_Sorting_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<AppointmentsController> _logger;
        public AppointmentsController(IAppointmentService appointmentService, 
            ILogger<AppointmentsController> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }
        [Authorize(Roles="Admin,Doctor,Receptionist")]
        [HttpGet]
        public async Task<IActionResult> GetAppointments([FromQuery] AppointmentFilterRequestDto filter)
        {
            _logger.LogInformation($"GetAppointments API called with PageNumber: {filter.PageNumber}, " +
                $"PageSize: {filter.PageSize}, DoctorId: {filter.DoctorId}, PatientId: {filter.PatientId}," +
                $" Status: {filter.AppointmentStatus}");

            var result = await _appointmentService.GetPagedAppointmentsAsync(filter);
            if (!result.Success)
            {
                _logger.LogWarning($"GetAppointments Validation failed: {result.Message}");
                return BadRequest(new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = result.Message
                });
            }
            _logger.LogInformation($"GetAppointments API successful. Returned {result.Data?.Data.Count ?? 0} records.");
            return Ok(result.Data);
        }
        [Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteAppointment(int id)
        {
            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK,
                Message = $"Only admin can delete Appointment id {id}"
            });
        }

        [Authorize(Roles="Admin,Receptionist")]
        [HttpPost]
        public IActionResult CreateAppointment()
        {
            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Only admin and receptionsit can create appointment."
            });
        }
        [Authorize(Roles = "Admin,Doctor,Receptionist,Patient")]
        [HttpGet("{id}")]
        public IActionResult GetAppointmentById(int id) {
            return Ok(new
            {
                StatusCode = StatusCodes.Status200OK,
                Message = "Authenticated users can view appointment id : {id}."
            });
                }
    }
}
