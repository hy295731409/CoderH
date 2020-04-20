using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Object.WeatherForecast
{
    public class WeatherForecastOutput
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        /// 温度（摄氏度）
        /// </summary>
        public int TemperatureC { get; set; }
        /// <summary>
        /// 温度（华氏度）
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary>
        /// 描述
        /// </summary>
        public string Summary { get; set; }
    }
}
