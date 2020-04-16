using Domain.Interface;
using Domain.Object.WeatherForecast;
using Entity;
using Framework.DB.Base;
using Framework.DB.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Implement
{
    public class WeatherForecastService : Repository<WeatherForecast>, IWeatherForecastService
    {
		public WeatherForecastService(ILogger<WeatherForecastService> logger) : base(logger)
		{
			
		}
		public Result<List<WeatherForecastOutput>> Test(TestInput input)
        {
			try
			{
				throw new Exception("test");
				return Result<List<WeatherForecastOutput>>.Success();
			}
			catch (Exception e)
			{
				_logger.LogError(e,e.Message);
				return Result<List<WeatherForecastOutput>>.Error(e.Message);
			}
        }
    }
}
