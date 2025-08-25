using System.ComponentModel.DataAnnotations;

namespace TodoApi.Validations;

public class FutureOrTodayDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not DateTime)
            return ValidationResult.Success;

        DateTime dateValue = (DateTime)value;

        return dateValue.Date < DateTime.Today
             ?new ValidationResult(ErrorMessage ?? "Date must be today or a future date")
             : ValidationResult.Success;
    }
}
