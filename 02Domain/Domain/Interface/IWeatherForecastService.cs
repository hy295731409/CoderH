using Domain.Object.WeatherForecast;
using Entity;
using Framework.DB.Base;
using Framework.DB.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IWeatherForecastService : IDependency,IRepository<WeatherForecast>
    {
        Result<List<WeatherForecastOutput>> Test(TestInput input);
        Task<WeatherForecastOutput> GetByIdAsync(int id, CancellationToken token);
    }
}
