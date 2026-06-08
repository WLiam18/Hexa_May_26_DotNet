using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalAppoinmentConsoleApp.Data;
using HospitalAppoinmentConsoleApp.Models;
using HospitalAppoinmentConsoleApp.Repositories.Interfaces;

namespace HospitalAppoinmentConsoleApp.Repositories.Implementations
{
    internal class AppointmentRepository : IAppointmentRepository
    {
        private readonly HospitalAppointmentDbContext _context;
        public AppointmentRepository(HospitalAppointmentDbContext context)
        {
            _context = context;
        }

        public List<Appointment> GetAllAppointments()
        {
            return _context.Appointments.ToList();
        }
        public Appointment? GetAppointmentById(int appointmentId)
        {
            return _context.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
        }
        public void AddAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
        }
        public void RemoveAppointment(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
            _context.SaveChanges();
        }
        public void UpdateAppointment(Appointment appointment)
        {
            var existingAppointment = _context.Appointments.FirstOrDefault(a => a.AppointmentId == appointment.AppointmentId);
            if (existingAppointment != null)
            {
                existingAppointment.DoctorId = appointment.DoctorId;
                existingAppointment.PatientId = appointment.PatientId;
                existingAppointment.AppointmentDate = appointment.AppointmentDate;
                existingAppointment.Symptoms = appointment.Symptoms;
                existingAppointment.AppointmentStatus = appointment.AppointmentStatus;
                _context.SaveChanges();
            }
        }
    }
}
