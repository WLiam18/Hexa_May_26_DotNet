using HospitalAppoinmentConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppoinmentConsoleApp.Repositories.Interfaces
{
    public interface IDoctorRepository
    {
        List<Doctor> GetAllDoctors();
        Doctor? GetDoctorById(int doctorId);
        void AddDoctor(Doctor doctor);
        void DeleteDoctor(int doctorId);
       void UpdateDoctor(Doctor doctor);
    }
}
