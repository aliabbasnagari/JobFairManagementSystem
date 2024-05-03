using System.ComponentModel.DataAnnotations;
using JobFairManagementSystem.Models;

namespace JobFairManagementSystem.CustomAttributes;

public class TimeCheck : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var slot = (Slot)validationContext.ObjectInstance;
        return validationContext.MemberName switch
        {
            nameof(slot.StartTime) when slot.StartTime.Hour is < 8 or > 18 => new ValidationResult("Start Time should be between 08:00 AM - 06:00 PM"),
            nameof(slot.EndTime) when slot.EndTime <= slot.StartTime => new ValidationResult("End Time should be Greater than Start Time."),
            nameof(slot.EndTime) when slot.EndTime.Hour > 18 => new ValidationResult("End Time should be between " + slot.StartTime.ToString("hh:mm tt") + " - 06:00 PM"),
            _ => ValidationResult.Success
        };
    }
}