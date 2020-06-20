using System;
using System.ComponentModel.DataAnnotations;

namespace EventsCore.WebUI.Models.Validators
{
    public class DateMustBeFuture : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateValue = value as DateTime? ?? new DateTime();
            if (dateValue < DateTime.Now)
            {
                return new ValidationResult("Must be a future date.");
            }
            return ValidationResult.Success;
        }
    }
}
