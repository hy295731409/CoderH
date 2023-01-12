using Microsoft.AspNetCore.Mvc;

namespace Net6WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost,Route("Create")]
        public AlarmTraceSendResponse Create([FromBody]Alarm alarm)
        {
            HttpContext.Request.Headers.TryGetValue("magicToken",out var value);

            return new AlarmTraceSendResponse()
            {
                Data = true,
                Success = true,
            };
        }
    }

    public class AlarmTraceSendResponse
    {
        public bool Success { get; set; }
        public bool Data { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public int ErrorNumber { get; set; }
    }
}