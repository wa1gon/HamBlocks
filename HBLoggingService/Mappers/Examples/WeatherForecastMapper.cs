using FastEndpoints;
using HBLoggingService.Models.Examples;
using HBLoggingService.Requests.Examples;
using HBLoggingService.Responses.Examples;

namespace HBLoggingService.Mappers.examples;

public class WeatherForecastMapper: Mapper<WeatherForecastRequest, WeatherForecastResponse, WeatherForecast>
{
    public override WeatherForecastResponse FromEntity(WeatherForecast e)
    {
        return new WeatherForecastResponse
        {
            Date = e.Date,
            TemperatureC = e.TemperatureC,
            TemperatureF = e.TemperatureF,
            Summary = e.Summary
        };
    }
}
