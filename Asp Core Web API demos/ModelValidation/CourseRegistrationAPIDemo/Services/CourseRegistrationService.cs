using CourseRegistrationAPIDemo.Dtos;
using CourseRegistrationAPIDemo.Models;

namespace CourseRegistrationAPIDemo.Services
{
    public class CourseRegistrationService : ICourseRegistrationService
    {
      //  private readonly ICourseRegistrationService _courseRegistrationService;
        private readonly List<CourseRegistration> Registrations = new();
        public CourseRegistration RegisterStudent(CourseRegistrationCreateDto dto)
        {
            var registration = new CourseRegistration
            {
                CourseRegistrationId = Registrations.Count + 1,
                StudentName = dto.StudentName,
                Email = dto.Email,
                MobileNumber = dto.MobileNumber,
                Age = dto.Age,
                CourseName = dto.CourseName,
                PaymentAmount = dto.PaymentAmount,
                TrainingMode = dto.TrainingMode,
                Location = dto.Location,
                CourseStartDate = dto.CourseStartDate,
                RegisteredOn = DateTime.Now
            };

            Registrations.Add(registration);

            return registration;
        }
    }
    }
    

