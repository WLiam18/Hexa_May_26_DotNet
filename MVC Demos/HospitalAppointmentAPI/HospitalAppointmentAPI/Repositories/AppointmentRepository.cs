using System.Collections.Generic;
using System.Threading.Tasks;
using HospitalAppointmentAPI.Data;
using HospitalAppointmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalAppointmentAPI.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HospitalAppointmentDbContext _db;

        public AppointmentRepository(HospitalAppointmentDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _db.Appointments
                .AsNoTracking()
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _db.Appointments
                .AsNoTracking()
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);
        }

        public async Task<Appointment> CreateAsync(Appointment appointment)
        {
            _db.Appointments.Add(appointment);
            await _db.SaveChangesAsync();
            // reload with includes
            return await GetByIdAsync(appointment.AppointmentId) ?? appointment;
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _db.Appointments.Update(appointment);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Appointment appointment)
        {
            _db.Appointments.Remove(appointment);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _db.Appointments.AnyAsync(a => a.AppointmentId == id);
        }
    }
}
