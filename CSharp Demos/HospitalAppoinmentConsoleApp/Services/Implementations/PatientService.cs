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
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }
        public void DisplayAllPatients()
        {
            List<Patient> patients = _patientRepository.GetAllPatients();
            if (patients.Count == 0)
            {
                Console.WriteLine("No patients found.");
                return;
            }
            foreach (var patient in patients)
            {
                Console.WriteLine($"Patient ID: {patient.PatientId}, Name: {patient.PatientName}");
            }
        }

        public void DisplayPatientById(int patientId) {
            Patient? patient = _patientRepository.GetPatientById(patientId);
            if (patient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }
            Console.WriteLine($"Patient ID: {patient.PatientId}, Name: {patient.PatientName}");
        }
       

        public void RegisterPatient(Patient patient)
        {
            if(string.IsNullOrWhiteSpace(patient.PatientName))
            {
                Console.WriteLine("Patient name cannot be empty.");
                return;
            }
            _patientRepository.AddPatient(patient);
            Console.WriteLine("Patient registered successfully.");
        }

        public void ModifyPatient(Patient patient)
        {
            Patient? existingPatient = _patientRepository.GetPatientById(patient.PatientId);
           if(existingPatient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }
            _patientRepository.UpdatePatient(patient);
            Console.WriteLine("Patient Details updated successfully.");
        }

        public void RemovePatient(int patientId)
        {
            Patient? existingPatient = _patientRepository.GetPatientById(patientId);
            if (existingPatient == null)
            {
                Console.WriteLine("Patient not found.");
                return;
            }
            _patientRepository.DeletePatient(patientId);
            Console.WriteLine("Patient removed successfully.");
        }
    }
}
