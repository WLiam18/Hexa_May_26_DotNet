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
    public class PatientRepository: IPatientRepository
    {
        private readonly HospitalAppointmentDbContext _context;
        public PatientRepository(HospitalAppointmentDbContext context)
        {
            _context = context;
        }
        public List<Patient> GetAllPatients()
        {
            return _context.Patients.ToList();
        }

        public Patient? GetPatientById(int patientId)
        {
            return _context.Patients.FirstOrDefault(p => p.PatientId == patientId);
        }

        public void AddPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            Console.WriteLine(); _context.SaveChanges();
        }
        public void UpdatePatient(Patient patient)
        {
            var existingPatient = _context.Patients.FirstOrDefault(p => p.PatientId == patient.PatientId);
            if (existingPatient != null)
            {
                existingPatient.PatientName = patient.PatientName;
                existingPatient.Age = patient.Age;
                existingPatient.Email = patient.Email;
                _context.SaveChanges();
            }
        }
        public void DeletePatient(int patientId)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
                _context.SaveChanges();
            }
        }
    }
}
