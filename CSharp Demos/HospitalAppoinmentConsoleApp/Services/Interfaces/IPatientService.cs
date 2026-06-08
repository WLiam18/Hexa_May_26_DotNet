using HospitalAppoinmentConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppoinmentConsoleApp.Services.Interfaces
{
    public interface IPatientService
    {
        void DisplayAllPatients();
        void DisplayPatientById(int patientId);
       // void DisplayPatientByName(string name);
        void RegisterPatient(Patient patient);
        void ModifyPatient(Patient patient);
        void RemovePatient(int patientId);

    }
}
