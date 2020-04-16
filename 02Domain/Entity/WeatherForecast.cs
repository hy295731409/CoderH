using Dapper.Contrib.Extensions.TZ;
using Framework.DB.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    [Table("weatherforecast")]
    public class WeatherForecast : DbEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }
    }
}
