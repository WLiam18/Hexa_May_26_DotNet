using Api_Pagination_Sorting_Demo.Data;
using Api_Pagination_Sorting_Demo.Models;
using Api_Pagination_Sorting_Demo.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Pagination_Sorting_Demo.Repository.Implementations
{
    public class AppointmentRepository: IAppointmentRepository
    {
        private readonly HospitalAppointmentDbContext _context;

        public AppointmentRepository(HospitalAppointmentDbContext context)
        {
            _context = context;
        }

        public IQueryable<Appointment> GetAllAppointmentsQueryable()
        {
            return _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .AsQueryable();
        }
    }
}
