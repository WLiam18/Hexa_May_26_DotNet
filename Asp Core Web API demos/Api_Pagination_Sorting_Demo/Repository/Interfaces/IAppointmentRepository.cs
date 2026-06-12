using Api_Pagination_Sorting_Demo.Models;

namespace Api_Pagination_Sorting_Demo.Repository.Interfaces
{
    public interface IAppointmentRepository
    {
        IQueryable<Appointment> GetAllAppointmentsQueryable();
    }
}
