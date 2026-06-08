using HospitalAppoinmentConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppoinmentConsoleApp.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {
        List<Appointment> GetAllAppointments();
        Appointment? GetAppointmentById(int appointmentId);
        void AddAppointment(Appointment appointment);
        void RemoveAppointment(Appointment appointment);
        void UpdateAppointment(Appointment appointment);
    }
}
