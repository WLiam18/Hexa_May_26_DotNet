using Hospital.PatientService.Models;

namespace Hospital.PatientService.Repositories.Interfaces
{
    public interface IPatientRepository
    {
       
            Task<List<Patient>> GetAllAsync();

            Task<Patient?> GetByIdAsync(int id);

            Task AddAsync(Patient patient);

            void Update(Patient patient);

            void Delete(Patient patient);

            Task SaveChangesAsync();
        }
    }

