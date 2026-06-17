using HospitalAppointmentMvc.Dtos;
using HospitalAppointmentMvc.services.Interfaces;
using System.Text;
using System.Text.Json;

namespace HospitalAppointmentMvc.services.Implementations
{
    public class AuthApiService:IAuthApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public AuthApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            string jsonData=JsonSerializer.Serialize(loginRequestDto);

            StringContent content=new StringContent(jsonData,Encoding.UTF8,"application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("/api/Auth/login",content);

           if(!response.IsSuccessStatusCode)
            {
                return null;
            }
           string jsonResponse=await response.Content.ReadAsStringAsync();

            LoginResponseDto? result=JsonSerializer.Deserialize<LoginResponseDto>(jsonResponse,_jsonOptions);

            return result;
        }
    }
}
