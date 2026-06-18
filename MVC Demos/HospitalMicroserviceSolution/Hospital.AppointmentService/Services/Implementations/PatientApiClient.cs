using Hospital.AppointmentService.DTOs;
using Hospital.AppointmentService.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Hospital.AppointmentService.Services.Implementations
{
    public class PatientApiClient : IPatientApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonSerializerOptions _jsonOptions;

        public PatientApiClient(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<PatientDto?> GetPatientByIdAsync(int patientId)
        {
            AddAuthorizationHeader();

            HttpResponseMessage response =
                await _httpClient.GetAsync($"api/Patients/{patientId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<PatientDto>(json, _jsonOptions);
        }

        private void AddAuthorizationHeader()
        {
            string? authorizationHeader = _httpContextAccessor.HttpContext?
                .Request.Headers["Authorization"]
                .ToString();

            _httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(authorizationHeader) &&
                authorizationHeader.StartsWith("Bearer "))
            {
                string token = authorizationHeader.Replace("Bearer ", "");

                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
