using HospitalAppoinmentConsoleApp.Data;
using HospitalAppoinmentConsoleApp.Models;
using HospitalAppoinmentConsoleApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppoinmentConsoleApp.Repositories.Implementations
{
    public class DoctorRepository: IDoctorRepository
    {
        private readonly HospitalAppointmentDbContext _context;
        public DoctorRepository(HospitalAppointmentDbContext context)
        {
            _context = context;
        }
        public List<Doctor> GetAllDoctors()
        {
            return _context.Doctors.ToList();
        }
        public Doctor? GetDoctorById(int doctorId)
        {
            return _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
        }
        public void AddDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
        }
        public void UpdateDoctor(Doctor doctor)
        {
            var existingDoctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == doctor.DoctorId);
            if (existingDoctor != null)
            {
                existingDoctor.DoctorName = doctor.DoctorName;
                existingDoctor.Specialization = doctor.Specialization;
                existingDoctor.ConsultationFee = doctor.ConsultationFee;
                existingDoctor.PhoneNumber = doctor.PhoneNumber;
                existingDoctor.IsAvailable = doctor.IsAvailable;
                _context.SaveChanges();
            }
        }
        public void DeleteDoctor(int doctorId)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.DoctorId == doctorId);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
            }
        }
    }
}
