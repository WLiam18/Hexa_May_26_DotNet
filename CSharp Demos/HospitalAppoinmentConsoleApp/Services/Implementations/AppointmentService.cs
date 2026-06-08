using HospitalAppoinmentConsoleApp.Models;
using HospitalAppoinmentConsoleApp.Repositories.Implementations;
using HospitalAppoinmentConsoleApp.Repositories.Interfaces;
using HospitalAppoinmentConsoleApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppoinmentConsoleApp.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        public void DisplayAllAppointments()
        {
            var appointments = _appointmentRepository.GetAllAppointments();
            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments found.");
                return;
            }
            foreach (var appointment in appointments)
            {
                Console.WriteLine($"ID: {appointment.AppointmentId}, Doctor ID: {appointment.DoctorId}, Patient ID: {appointment.PatientId}, Date: {appointment.AppointmentDate}, Symptoms: {appointment.Symptoms}, Status: {appointment.AppointmentStatus}");
            }
        }

        public void DisplayAppointmentById(int appointmentId)
        {
            Appointment? appointment = _appointmentRepository.GetAppointmentById(appointmentId);
            if (appointment == null)
            {
                Console.WriteLine("No appointments found.");
                return;
            }
            Console.WriteLine($"ID: {appointment.AppointmentId}, Doctor ID: {appointment.DoctorId}, Patient ID: {appointment.PatientId}, Date: {appointment.AppointmentDate}, Symptoms: {appointment.Symptoms}, Status: {appointment.AppointmentStatus}");

        }

        public void BookAppointment(Appointment appointment)
        {
            if (appointment.PatientId <= 0)
            {
                Console.WriteLine("Invalid patient ID. Please provide a valid patient ID.");
                return;
            }
            if(appointment.DoctorId<=0) {
                Console.WriteLine("Invalid doctor ID. Please provide a valid doctor ID.");
                return;
            }
            _appointmentRepository.AddAppointment(appointment);
            Console.WriteLine("Appointment booked successfully.");
        }

        public void ModifyAppointment(Appointment appointment)
        {
            Appointment? existingAppointment = _appointmentRepository.GetAppointmentById(appointment.AppointmentId);
            if(existingAppointment == null) {
                Console.WriteLine("Appointment not found.");
                return;
            }
            existingAppointment.PatientId = appointment.PatientId;
            existingAppointment.DoctorId = appointment.DoctorId;
            existingAppointment.AppointmentDate = appointment.AppointmentDate;

            _appointmentRepository.UpdateAppointment(existingAppointment);
            Console.WriteLine("Appointment modified successfully.");
        }

        public void CancelAppointment(int appointmentId)
        {
            Appointment? existingAppointment = _appointmentRepository.GetAppointmentById(appointmentId);
            if (existingAppointment == null)
            {
                Console.WriteLine("Appointment not found.");
                return;
            }
            existingAppointment.AppointmentStatus = "Cancelled";
            _appointmentRepository.UpdateAppointment(existingAppointment);
            Console.WriteLine("Appointment cancelled successfully.");
        }

       
    }
}
