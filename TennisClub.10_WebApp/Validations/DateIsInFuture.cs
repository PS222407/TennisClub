using System.ComponentModel.DataAnnotations;

namespace TennisClub_0._1.Validations;

public class DateIsInFuture : ValidationAttribute
{
    public override string FormatErrorMessage(string name)
    {
        return "Date value should be a future date";
    }

    protected override ValidationResult? IsValid(object? objValue, ValidationContext validationContext)
    {
        DateTime dateValue = objValue as DateTime? ?? new DateTime();

        return DateTime.Now.Date > dateValue.Date ? new ValidationResult(FormatErrorMessage(validationContext.DisplayName)) : ValidationResult.Success;
    }
}