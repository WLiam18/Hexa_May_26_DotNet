using HospitalAppoinmentConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppoinmentConsoleApp.Services.Interfaces
{
    public interface IDoctorService
    {
        void DisplayAllDoctors();
        void DisplayDoctorById(int doctorId);
        void RegisterDoctor(Doctor doctor);
        void ModifyDoctor(Doctor doctor);
        void RemoveDoctor(int doctorId);
    }
}
