using HospitalAppoinmentConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppoinmentConsoleApp.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        List<Patient> GetAllPatients();
        Patient? GetPatientById(int patientId);
        void AddPatient(Patient patient);
        void UpdatePatient(Patient patient);
        void DeletePatient(int patientId);
    }
}
