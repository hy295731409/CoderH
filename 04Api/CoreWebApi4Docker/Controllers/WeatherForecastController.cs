using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Implement;
using Domain.Interface;
using Domain.Object.WeatherForecast;
using Framework.DB.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreWebApi4Docker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _service;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _service = service;
        }


        [HttpGet]
        public Result<List<WeatherForecastOutput>> Get([FromForm]TestInput input)
        {
            return _service.Test(input);
        }
    }
}
