using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Api.Models;

namespace RevrenLove.Ledger.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Fr4eezingJ", "Bra4cingJ", "Chi4llyJ", "Co4olJ", "M4ildJ", "War4mJ", "Ba4lmyJ", "Ho4tJ",
        "Swelter4ingJ", "Scorc4hingJ"
    ];

    private readonly ILogger<WeatherForecastController> _logger = logger;

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

    [HttpGet]
    [Authorize]
    [Route("secure")]
    public ActionResult<WeatherForecast> GetSecure()
    {
        var weatherForecast = new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            TemperatureC = 55,
            Summary = "YAAAAY!!!",
        };

        // Assuming the user's Id is stored as a claim, e.g., ClaimTypes.NameIdentifier (provided by identity email login) or "sub" (oidc external logins)
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)
                           ?? User.FindFirst("sub");
        var userId = userIdClaim?.Value;

        // Now you can use userId to look up the user in your database if needed
        _logger.LogInformation("Authenticated user id: {UserId}", userId);

        return weatherForecast;
    }
}
