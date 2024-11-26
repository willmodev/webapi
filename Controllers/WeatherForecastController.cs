using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private static List<WeatherForecast> _weatherForecasts = new List<WeatherForecast>();

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;

        if (_weatherForecasts.Count == 0 || !_weatherForecasts.Any())
        {
            _weatherForecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToList();
        }
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _weatherForecasts;
    }


    [HttpPost(Name = "AddWeatherForecast")]
    public IActionResult Post([FromBody] WeatherForecast weatherForecast)
    {
        _weatherForecasts.Add(weatherForecast);
        return CreatedAtRoute("GetWeatherForecast", new { }, weatherForecast);
    }

    [HttpDelete("{position}", Name = "DeleteWeatherForecast")]
    public IActionResult Delete(int position)
    {
        if (position < 0 || position >= _weatherForecasts.Count)
        {
            return NotFound();
        }

        _weatherForecasts.RemoveAt(position);
        return NoContent();
    }
    
}
