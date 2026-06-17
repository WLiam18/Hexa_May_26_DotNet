using HospitalAppointmentMvc.Dtos;
using HospitalAppointmentMvc.services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthApiService _authApiService;
        public AuthController(IAuthApiService authApiService)
        {
            _authApiService = authApiService;

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            LoginResponseDto? result = await _authApiService.LoginAsync(model);

            if (result == null || !result.Success || string.IsNullOrEmpty(result.Token))
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            HttpContext.Session.SetString("JWToken", result.Token);

            return RedirectToAction("Index", "Appointments");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");

            return RedirectToAction("Login");
        }
    }
}
