using CourseRegistrationAPIDemo.Dtos;
using FluentValidation;

namespace CourseRegistrationAPIDemo.Validations
{
    public class CourseRegistrationCreateDtoValidator:AbstractValidator<CourseRegistrationCreateDto>
    {
        public CourseRegistrationCreateDtoValidator()
        {
            RuleFor(x => x.StudentName)
                .NotEmpty()
                .WithMessage("Student name is required.")
                .Length(3, 30)
                .WithMessage("Student name must be between 3 and 30 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email address.")
                .Must(email => email != null && email.EndsWith(".com", StringComparison.OrdinalIgnoreCase))
                .WithMessage("Email must end with .com.");

            RuleFor(x => x.MobileNumber)
                .NotEmpty()
                .WithMessage("Mobile number is required.")
                .Matches(@"^[6-9]\d{9}$")
                .WithMessage("Mobile number must be a valid 10 digit Indian mobile number.");

            RuleFor(x => x.Age)
                .InclusiveBetween(18, 60)
                .WithMessage("Age must be between 18 and 60.");

            RuleFor(x => x.CourseName)
                .NotEmpty()
                .WithMessage("Course name is required.")
                .MaximumLength(100)
                .WithMessage("Course name cannot exceed 100 characters.");

            RuleFor(x => x.PaymentAmount)
                .InclusiveBetween(10000, 200000)
                .WithMessage("Payment amount must be between 10,000 and 2,00,000.");

            RuleFor(x => x.TrainingMode)
                .NotEmpty()
                .WithMessage("Training mode is required.")
                .Must(mode => mode == "Online" || mode == "Offline")
                .WithMessage("Training mode must be Online or Offline.");

            RuleFor(x => x.Location)
                .NotEmpty()
                .When(x => x.TrainingMode == "Offline")
                .WithMessage("Location is required for offline training.");

            RuleFor(x => x.CourseStartDate)
                .Must(date => date.Date > DateTime.Today)
                .WithMessage("Course start date must be a future date.");
        }
    }
}
