using HBLoggingService.Requests;
using HBLoggingService.Responses;
using FastEndpoints;
using HBLoggingService.Mappers;
using HBLoggingService.Mappers.examples;
using HBLoggingService.Models;
using HBLoggingService.Models.Examples;
using HBLoggingService.Requests.Examples;
using HBLoggingService.Responses.Examples;
using Microsoft.AspNetCore.Identity;
namespace HBLoggingService.Endpoints.Examples;

public class WeatherForecastEndpoint: Endpoint<WeatherForecastRequest,WeatherForecastsResponse, WeatherForecastMapper>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    // public ILogger<WeatherForecastEndpoint> Logger { get; init; }
    public override void Configure()
    {
        Verbs(Http.GET);
        Routes("weather/{days}");
        AllowAnonymous();
        // Doesn't work with FastEndpoints 7.0.0
        // Describe(x => x
        //     .Produces<WeatherForecastsResponse>(StatusCodes.Status200OK)
        //     .Produces(StatusCodes.Status400BadRequest)
        //     .WithName("GetWeatherForecast")
        //     .WithSummary("Get weather forecast for a specified number of days")
        //     .WithDescription("Returns a list of weather forecasts for the next specified number of days."));


    }

    public override async Task HandleAsync(WeatherForecastRequest req,CancellationToken ct)
    {
        int days = req.Days > 0 ? req.Days : 5; // Default to 5 days if not specified
        var forecasts = Enumerable.Range(1, days).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        }).ToArray();

        var response = new WeatherForecastsResponse
        {
            Forecasts = forecasts.Select(f => new WeatherForecastMapper().FromEntity(f)).ToList()
        };
            
        // var response = new WeatherForecastsResponse
        // {
        //     Forecasts = forecasts.Select(f => new WeatherForecastResponse
        //     {
        //         Date = f.Date,
        //         TemperatureC = f.TemperatureC,
        //         TemperatureF = 32 + (int)(f.TemperatureC / 0.5556),
        //         Summary = f.Summary
        //     }).ToList()
        // };
        await SendAsync(response, cancellation: ct);

    } 
}
