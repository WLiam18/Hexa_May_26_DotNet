using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAppointmentAPI.Data;
using HospitalAppointmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentAPI.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalAppointmentDbContext _db;

        public PatientRepository(HospitalAppointmentDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            return await _db.Patients.AsNoTracking().ToListAsync();
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _db.Patients.AsNoTracking().FirstOrDefaultAsync(p => p.PatientId == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _db.Patients.AnyAsync(p => p.PatientId == id);
        }
    }
}
