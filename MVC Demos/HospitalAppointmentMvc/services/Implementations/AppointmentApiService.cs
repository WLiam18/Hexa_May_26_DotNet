using HospitalAppointmentMvc.Dtos;
using HospitalAppointmentMvc.services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HospitalAppointmentMvc.services.Implementations
{
    public class AppointmentApiService:IAppointmentApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonSerializerOptions _jsonOptions;

        public AppointmentApiService(HttpClient httpClient, IHttpContextAccessor contextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = contextAccessor;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        private void AddJwtToken()
        {
            string? token = _httpContextAccessor.HttpContext?.Session.GetString("JWToken");
            _httpClient.DefaultRequestHeaders.Authorization = null;
            if(!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
        public async Task<List<AppointmentResponseDto>> GetAllAppointmentsAsync()
        {
            AddJwtToken();

            HttpResponseMessage response = await _httpClient.GetAsync("api/Appointments");

            if(!response.IsSuccessStatusCode)
            {
                return new List<AppointmentResponseDto>();
            }
            string jsonResponse=await response.Content.ReadAsStringAsync();

            List<AppointmentResponseDto> appointments = JsonSerializer.
                Deserialize<List<AppointmentResponseDto>>(jsonResponse, _jsonOptions);

            return appointments ?? new List<AppointmentResponseDto>();
        }
        public async Task<AppointmentResponseDto?> GetAppointmentByIdAsync(int id)
        {
            AddJwtToken();

            HttpResponseMessage response =
                await _httpClient.GetAsync($"api/Appointments/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();

            AppointmentResponseDto? appointment =
                JsonSerializer.Deserialize<AppointmentResponseDto>(jsonResponse, _jsonOptions);

            return appointment;
        }

        public async Task<bool> CreateAppointmentAsync(AppointmentCreateDto appointment)
        {
            AddJwtToken();

            string jsonData = JsonSerializer.Serialize(appointment);

            StringContent content = new StringContent(
                jsonData,
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage response =
                await _httpClient.PostAsync("api/Appointments", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAppointmentStatusAsync(
            int id,
            AppointmentUpdateStatusDto statusDto)
        {
            AddJwtToken();

            string jsonData = JsonSerializer.Serialize(statusDto);

            StringContent content = new StringContent(
                jsonData,
                Encoding.UTF8,
                "application/json");

            HttpResponseMessage response =
                await _httpClient.PutAsync($"api/Appointments/{id}/status", content);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAppointmentAsync(int id)
        {
            AddJwtToken();

            HttpResponseMessage response =
                await _httpClient.DeleteAsync($"api/Appointments/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<SelectList> GetDoctorsSelectListAsync(int? selectedDoctorId = null)
        {
            AddJwtToken();

            HttpResponseMessage response = await _httpClient.GetAsync("api/Doctors");

            if (!response.IsSuccessStatusCode)
            {
                return new SelectList(new List<DoctorResponseDto>(), "DoctorId", "FullName");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();

            List<DoctorResponseDto>? doctors =
                JsonSerializer.Deserialize<List<DoctorResponseDto>>(jsonResponse, _jsonOptions);

            return new SelectList(
                doctors ?? new List<DoctorResponseDto>(),
                "DoctorId",
                "FullName",
                selectedDoctorId);
        }

        public async Task<SelectList> GetPatientsSelectListAsync(int? selectedPatientId = null)
        {
            AddJwtToken();

            HttpResponseMessage response = await _httpClient.GetAsync("api/Patients");

            if (!response.IsSuccessStatusCode)
            {
                return new SelectList(new List<PatientResponseDto>(), "PatientId", "FullName");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();

            List<PatientResponseDto>? patients =
                JsonSerializer.Deserialize<List<PatientResponseDto>>(jsonResponse, _jsonOptions);

            return new SelectList(
                patients ?? new List<PatientResponseDto>(),
                "PatientId",
                "FullName",
                selectedPatientId);
        }
    }

}

