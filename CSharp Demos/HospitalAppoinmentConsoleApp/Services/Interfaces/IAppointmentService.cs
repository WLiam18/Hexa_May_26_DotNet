using HospitalAppoinmentConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppoinmentConsoleApp.Services.Interfaces
{
    public  interface IAppointmentService
    {
        void DisplayAllAppointments();
        void DisplayAppointmentById(int appointmentId);
        void BookAppointment(Appointment appointment);
        void ModifyAppointment(Appointment appointment);
        void CancelAppointment(int appointmentId);
    }
}
