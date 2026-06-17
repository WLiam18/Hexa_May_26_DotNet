using System.Threading.Tasks;
using HospitalAppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Get()
        {
            var list = await _patientService.GetAllAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        [Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _patientService.GetByIdAsync(id);
            if (p == null) return NotFound();
            return Ok(p);
        }
    }
}
