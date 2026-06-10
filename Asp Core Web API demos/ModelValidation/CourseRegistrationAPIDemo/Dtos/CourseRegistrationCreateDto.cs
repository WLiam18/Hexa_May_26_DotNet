using CourseRegistrationAPIDemo.Validations;
using System.ComponentModel.DataAnnotations;

namespace CourseRegistrationAPIDemo.Dtos
{
    public class CourseRegistrationCreateDto
    {
        [Required(ErrorMessage="Student Name is required.")]
        [StringLength(30,MinimumLength =3,ErrorMessage ="StrundentName must between 3 and 30 characters")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage="Email is required.")]
        [EmailAddress(ErrorMessage="Invalid email address")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage="Mobile Number is required.")]
        [RegularExpression(@"^[6-9]\d{9}$",ErrorMessage ="MobileNumber must be a valid 10 digit indian mobile number")]
        public string MobileNumber { get; set; } = string.Empty;

        [Range(18,60,ErrorMessage="Age must be betwen 18 to 60.")]
        public int Age { get; set; }

        [Required(ErrorMessage="Course name is required.")]
        [StringLength(100,ErrorMessage="course name cannot exceed 100 characters.")]
        public string CourseName { get; set; } = string.Empty;

        [Range(10000,200000,ErrorMessage="Payment amount must be between 10k to 2Lakhs.")]
        public decimal PaymentAmount { get; set; }

        [Required(ErrorMessage="Training mode is required.")]
        [RegularExpression("Online|Offline",ErrorMessage="Training mode must be Online or Offline.")]
        public string TrainingMode { get; set; } = string.Empty;
        public string? Location { get; set; } = string.Empty;


        [FutureDate(ErrorMessage="Course star date must be a future date.")]
        public DateTime CourseStartDate { get; set; }
        public DateTime RegisteredOn { get; set; }
    }
}
