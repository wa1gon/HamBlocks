using FastEndpoints;
using FastEndpointServer.Responses;

namespace FastEndpointServer.Mappers;

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
