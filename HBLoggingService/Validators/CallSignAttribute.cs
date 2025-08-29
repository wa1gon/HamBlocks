using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;

namespace HBLoggingService.Validators;

public class CallsignAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var callsign = value as string;
        if (string.IsNullOrWhiteSpace(callsign))
            return new ValidationResult("Callsign is required.");

        // Add your complete validation logic here
        if (!IsValidCallsign(callsign))
            return new ValidationResult("Callsign format is invalid.");

        return ValidationResult.Success;
    }

    private bool IsValidCallsign(string callsign)
    {
        // Example: Add regex, length, prefix, etc.
        // return Regex.IsMatch(callsign, @"^[A-Z0-9\-]+$");
        // Add more rules as needed
        return true;
    }
}
