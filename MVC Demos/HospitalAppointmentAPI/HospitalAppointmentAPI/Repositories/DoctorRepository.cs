using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAppointmentAPI.Data;
using HospitalAppointmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentAPI.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalAppointmentDbContext _db;

        public DoctorRepository(HospitalAppointmentDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Doctor>> GetAllAsync()
        {
            return await _db.Doctors.AsNoTracking().ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _db.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.DoctorId == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _db.Doctors.AnyAsync(d => d.DoctorId == id);
        }
    }
}
