using System.ComponentModel.DataAnnotations;

namespace CourseRegistrationAPIDemo.Validations
{
    public class FutureDateAttribute:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Date is required.");
            if(value is DateTime dateValue)
            {
                if (dateValue.Date <= DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "Date must be future date.");
                }
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid date value.");

        }

    }
}
