using Api_Pagination_Sorting_Demo.Services.Interfaces;
using Api_Pagination_Sorting_Demo.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Pagination_Sorting_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        /// <summary>
        /// Authenticates a user and returns a login response.
        /// </summary>
        /// <param name="request">Login request DTO containing userName and password.</param>
        /// <returns>Login response DTO with userName, role, token, and message.</returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            // Validate input
            if (request == null || string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            var loginResponse = _authService.LoginAsync(request);

            if (loginResponse == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            // Return populated response DTO
            return Ok(loginResponse);
        }

    }
}
