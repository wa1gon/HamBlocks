using HBLoggingService.Requests.Examples;

namespace HBLoggingService.Validators.Examples;
using FastEndpoints;
using FluentValidation;
public class WeatherForecastRetrievalValidator: Validator<WeatherForecastRequest>
{
    public WeatherForecastRetrievalValidator()
    {

        // Assuming Date is a property in the request, if not, you can remove this part.
        // If Date is not part of the request, you can remove this rule.
        RuleFor(x => x.Days)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Date must be greater than or equal to 1.")
            .LessThanOrEqualTo(14)
            .WithMessage("Date must be less than or equal to 14.");
    }  
}
