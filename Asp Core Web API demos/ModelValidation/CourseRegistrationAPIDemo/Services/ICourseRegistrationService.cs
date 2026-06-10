using CourseRegistrationAPIDemo.Models;
using CourseRegistrationAPIDemo.Dtos;


namespace CourseRegistrationAPIDemo.Services
{
    public interface ICourseRegistrationService
    {
        CourseRegistration RegisterStudent(CourseRegistrationCreateDto courseRegistration);
    }
}
