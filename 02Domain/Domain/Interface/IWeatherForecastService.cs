using Domain.Object.WeatherForecast;
using Entity;
using Framework.DB.Base;
using Framework.DB.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interface
{
    public interface IWeatherForecastService : IDependency,IRepository<WeatherForecast>
    {
        Result<List<WeatherForecastOutput>> Test(TestInput input);
    }
}
