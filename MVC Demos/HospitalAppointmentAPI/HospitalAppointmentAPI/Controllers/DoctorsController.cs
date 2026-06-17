using System.Threading.Tasks;
using HospitalAppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get()
        {
            var list = await _doctorService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var d = await _doctorService.GetByIdAsync(id);
            if (d == null) return NotFound();
            return Ok(d);
        }
    }
}
