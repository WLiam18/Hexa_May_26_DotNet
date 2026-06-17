using System.Threading.Tasks;
using HospitalAppointmentAPI.DTOs;
using HospitalAppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _authService.AuthenticateAsync(dto);
            if (!result.Success) return Unauthorized(new { error = "Invalid credentials" });
            return Ok(result);
        }
    }
}
