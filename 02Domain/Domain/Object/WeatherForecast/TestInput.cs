using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Object.WeatherForecast
{
    public class TestInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }

        public IEnumerable<Person> Sons { get; set; }

        public class Person
        {
            string name { get; set; }
            int age { get; set; }
        }
    }

    
}
