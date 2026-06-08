using HospitalAppoinmentConsoleApp.Models;
using HospitalAppoinmentConsoleApp.Repositories.Interfaces;
using HospitalAppoinmentConsoleApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppoinmentConsoleApp.Services.Implementations
{
    public class DoctorService: IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public void DisplayAllDoctors()
        {
            List<Doctor> doctors = _doctorRepository.GetAllDoctors();
            if(doctors.Count==0)
            {
                Console.WriteLine("No doctors found.");
                return;
            }
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"ID: {doctor.DoctorId}, Name: {doctor.DoctorName}, Specialization: {doctor.Specialization}, Fee: {doctor.ConsultationFee}, Phone: {doctor.PhoneNumber}, Available: {doctor.IsAvailable}");
            }
        }

        public void DisplayDoctorById(int doctorId)
        {
            Doctor? doctor = _doctorRepository.GetDoctorById(doctorId);
            if (doctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }
            Console.WriteLine($"ID: {doctor.DoctorId}, Name: {doctor.DoctorName}, Specialization: {doctor.Specialization}, Fee: {doctor.ConsultationFee}, Phone: {doctor.PhoneNumber}, Available: {doctor.IsAvailable}");
        }
        public void RegisterDoctor(Doctor doctor)
        {
           if(string.IsNullOrEmpty(doctor.DoctorName) || string.IsNullOrEmpty(doctor.Specialization) || doctor.ConsultationFee <= 0 || string.IsNullOrEmpty(doctor.PhoneNumber))
            {
                Console.WriteLine("Invalid doctor details. Please provide all required information.");
                return;
            }
            
            _doctorRepository.AddDoctor(doctor);
            Console.WriteLine("Doctor registered successfully.");
        }

        public void ModifyDoctor(Doctor doctor)
        {
            Doctor? existingDoctor = _doctorRepository.GetDoctorById(doctor.DoctorId);
            if(existingDoctor == null) {
                Console.WriteLine("Doctor not found.");
                return;
            }
            _doctorRepository.UpdateDoctor(doctor);
            Console.WriteLine("Doctor details updated successfully.");
        }
        public void RemoveDoctor(int doctorId)
        {
            Doctor? existingDoctor = _doctorRepository.GetDoctorById(doctorId);
            if (existingDoctor == null)
            {
                Console.WriteLine("Doctor not found.");
                return;
            }
            _doctorRepository.DeleteDoctor(doctorId);
            Console.WriteLine("Doctor removed successfully.");
        }
    }
}
