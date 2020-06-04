using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Implement;
using Domain.Interface;
using Domain.Object.WeatherForecast;
using Framework.DB.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreWebApi4Docker.Controllers
{
    /// <summary>
    /// 天气controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ApiControllerBase
    {
        private readonly IWeatherForecastService _service;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取天气(只有角色为Testuser的用户能访问)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles ="TestUser")]
        public Result<List<WeatherForecastOutput>> Get([FromForm]TestInput input)
        {
            var data = CurrentUser;
            
            return _service.Test(input);
        }

        /// <summary>
        /// 添加天气信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Result<List<WeatherForecastOutput>> Add([FromBody]TestInput input)
        {
            return _service.Test(input);
        }
    }
}
