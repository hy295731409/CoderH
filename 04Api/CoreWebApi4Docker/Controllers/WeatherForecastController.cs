using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        ILogger<WeatherForecastController> _logger;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService service)
        {
            _service = service;
            _logger = logger;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token">浏览器取消请求时，服务端会将 HttpContext.RequestAborted 中的 Token 绑定到 Action 的 CancellationToken 参数。
        /// 我们只需在接口中增加参数 CancellationToken，并将其传入其他接口调用中，程序识别到令牌被取消就会自动放弃继续执行</param>
        /// <returns></returns>
        public async Task<WeatherForecastOutput> GetByIdAsync(int id,CancellationToken token)
        {
            var res = new WeatherForecastOutput();
            try
            {
                await _service.GetByIdAsync(id, token);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            return res;
        }

        [HttpGet,Route("ViewTest")]
        public IActionResult ViewTest()
        {
            var ret = new ViewResult();
            ret.ViewName = "ViewTest";
            return ret;
        }
    }
}
