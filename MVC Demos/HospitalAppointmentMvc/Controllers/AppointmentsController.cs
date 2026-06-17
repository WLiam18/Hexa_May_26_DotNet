using HospitalAppointmentMvc.Dtos;
using HospitalAppointmentMvc.services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HospitalAppointmentMvc.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentApiService _appointmentApiService;

        public AppointmentsController(IAppointmentApiService appointmentApiService)
        {
            _appointmentApiService = appointmentApiService;
        }
        private bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(HttpContext.Session.GetString("JWToken"));
        }

        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            List<AppointmentResponseDto> appointments =
                await _appointmentApiService.GetAllAppointmentsAsync();

            return View(appointments);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            AppointmentResponseDto? appointment =
                await _appointmentApiService.GetAppointmentByIdAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            ViewBag.Doctors = await _appointmentApiService.GetDoctorsSelectListAsync();
            ViewBag.Patients = await _appointmentApiService.GetPatientsSelectListAsync();

            AppointmentCreateDto model = new AppointmentCreateDto
            {
                AppointmentDate = DateOnly.FromDateTime(DateTime.Today),
                AppointmentTime = TimeOnly.FromDateTime(DateTime.Now)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentCreateDto model)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Doctors = await _appointmentApiService.GetDoctorsSelectListAsync(model.DoctorId);
                ViewBag.Patients = await _appointmentApiService.GetPatientsSelectListAsync(model.PatientId);

                return View(model);
            }

            bool isCreated = await _appointmentApiService.CreateAppointmentAsync(model);

            if (!isCreated)
            {
                ModelState.AddModelError("", "Unable to create appointment. Check API role permission and API running status.");

                ViewBag.Doctors = await _appointmentApiService.GetDoctorsSelectListAsync(model.DoctorId);
                ViewBag.Patients = await _appointmentApiService.GetPatientsSelectListAsync(model.PatientId);

                return View(model);
            }

            TempData["SuccessMessage"] = "Appointment created successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            AppointmentResponseDto? appointment =
                await _appointmentApiService.GetAppointmentByIdAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            AppointmentUpdateStatusDto model = new AppointmentUpdateStatusDto
            {
                AppointmentStatus = appointment.AppointmentStatus
            };

            ViewBag.AppointmentId = id;
            ViewBag.Statuses = GetStatusSelectList(model.AppointmentStatus);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, AppointmentUpdateStatusDto model)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.AppointmentId = id;
                ViewBag.Statuses = GetStatusSelectList(model.AppointmentStatus);
                return View(model);
            }

            bool isUpdated =
                await _appointmentApiService.UpdateAppointmentStatusAsync(id, model);

            if (!isUpdated)
            {
                ModelState.AddModelError("", "Unable to update status. Check API role permission.");

                ViewBag.AppointmentId = id;
                ViewBag.Statuses = GetStatusSelectList(model.AppointmentStatus);

                return View(model);
            }

            TempData["SuccessMessage"] = "Appointment status updated successfully";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            AppointmentResponseDto? appointment =
                await _appointmentApiService.GetAppointmentByIdAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsLoggedIn())
            {
                return RedirectToAction("Login", "Auth");
            }

            bool isDeleted = await _appointmentApiService.DeleteAppointmentAsync(id);

            if (!isDeleted)
            {
                return BadRequest("Unable to delete appointment. Only Admin role can delete.");
            }

            TempData["SuccessMessage"] = "Appointment deleted successfully";

            return RedirectToAction(nameof(Index));
        }

        private SelectList GetStatusSelectList(string? selectedStatus = null)
        {
            List<string> statuses = new List<string>
            {
                "Scheduled",
                "Completed",
                "Cancelled"
            };

            return new SelectList(statuses, selectedStatus);
        }
    }
}

