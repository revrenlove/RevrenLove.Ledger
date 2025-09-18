using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Fr4eezingJ", "Bra4cingJ", "Chi4llyJ", "Co4olJ", "M4ildJ", "War4mJ", "Ba4lmyJ", "Ho4tJ",
        "Swelter4ingJ", "Scorc4hingJ"
    ];

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return [.. Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })];
    }
}
