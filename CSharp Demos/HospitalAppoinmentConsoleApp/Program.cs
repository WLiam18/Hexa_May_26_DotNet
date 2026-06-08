using HospitalAppoinmentConsoleApp.Data;
using HospitalAppoinmentConsoleApp.Models;
using HospitalAppoinmentConsoleApp.Repositories.Implementations;
using HospitalAppoinmentConsoleApp.Repositories.Interfaces;
using HospitalAppoinmentConsoleApp.Services.Implementations;
using HospitalAppoinmentConsoleApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HospitalAppoinmentConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection services = new ServiceCollection();

            services.AddDbContext<HospitalAppointmentDbContext>(options =>
            options.UseSqlServer("Server=localhost;Database=HospitalAppointmentDb;Trusted_Connection=true;"));
            // Register services here
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IDoctorService, DoctorService>();

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            IAppointmentService appointmentService = serviceProvider.GetRequiredService<IAppointmentService>();
            IPatientService patientService = serviceProvider.GetRequiredService<IPatientService>();
            IDoctorService doctorService = serviceProvider.GetRequiredService<IDoctorService>();

           

                bool continueApp = true;

                while (continueApp)
                {
                    Console.WriteLine();
                    Console.WriteLine("===== Hospital Appointment Console App =====");
                    Console.WriteLine("1. View All Patients");
                    Console.WriteLine("2. View All Doctors");
                    Console.WriteLine("3. View All Appointments");
                    Console.WriteLine("4. Add Patient");
                    Console.WriteLine("5. Add Doctor");
                    Console.WriteLine("6. Book Appointment");
                    Console.WriteLine("7. Exit");
                    Console.Write("Enter your choice: ");

                    string? choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            patientService.DisplayAllPatients();
                            break;

                        case "2":
                            doctorService.DisplayAllDoctors();
                            break;

                        case "3":
                            appointmentService.DisplayAllAppointments();
                            break;

                        case "4":
                            AddPatient(patientService);
                            break;

                        case "5":
                            AddDoctor(doctorService);
                            break;

                        case "6":
                            BookAppointment(appointmentService);
                            break;

                        case "7":
                            continueApp = false;
                            Console.WriteLine("Thank you for using Hospital Appointment Console App.");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
             
                }
            Console.ReadLine();
        }

            static void AddPatient(IPatientService patientService)
            {
            Patient patient = new Patient();
                Console.Write("Enter patient name: ");
               patient.PatientName = Console.ReadLine();
            Console.WriteLine("Enter the Patient Gender");
            patient.Gender = Console.ReadLine();
            Console.WriteLine("Enter the Patient Age");
            patient.Age = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the Patient Email");
            patient.Email = Console.ReadLine();
            Console.WriteLine("Enter the Patient Phone Number");
            patient.PhoneNumber = Console.ReadLine();
            Console.WriteLine("Enter the City");
            patient.City = Console.ReadLine();
            patientService.RegisterPatient(patient);
            Console.WriteLine("Patient added successfully.");
       
            }

            static void AddDoctor(IDoctorService doctorService)
            {
                Console.Write("Enter doctor name: ");
                string? doctorName = Console.ReadLine();

                Doctor doctor = new Doctor
                {
                    DoctorName = doctorName
                };

                doctorService.RegisterDoctor(doctor);
            }

        static void BookAppointment(IAppointmentService appointmentService)
        {
            Console.Write("Enter patient ID: ");
            if (!int.TryParse(Console.ReadLine(), out int patientId))
            {
                Console.WriteLine("Invalid patient ID.");
                return;
            }

            Console.Write("Enter doctor ID: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorId))
            {
                Console.WriteLine("Invalid doctor ID.");
                return;
            }

            Console.Write("Enter appointment date yyyy-MM-dd: ");
            if (!DateOnly.TryParse(Console.ReadLine(), out DateOnly appointmentDate))
            {
                Console.WriteLine("Invalid date format. Please enter date as yyyy-MM-dd.");
                return;
            }

            Console.Write("Enter appointment time HH:mm: ");
            if (!TimeOnly.TryParse(Console.ReadLine(), out TimeOnly appointmentTime))
            {
                Console.WriteLine("Invalid time format. Please enter time as HH:mm.");
                return;
            }

            Console.Write("Enter symptoms: ");
            string symptoms = Console.ReadLine() ?? string.Empty;

            Appointment appointment = new Appointment
            {
                PatientId = patientId,
                DoctorId = doctorId,
                AppointmentDate = appointmentDate,
                AppointmentTime = appointmentTime,
                AppointmentStatus = "Booked",
                Symptoms = symptoms,
                CreatedDate = DateTime.Now
            };

            appointmentService.BookAppointment(appointment);
        }

    }
    }

