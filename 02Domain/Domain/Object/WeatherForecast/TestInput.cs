using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Domain.Object.WeatherForecast
{
    public class TestInput
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Description("名称")]
        public string Name { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Description("年龄")]
        public int Age { get; set; }
        /// <summary>
        /// 孩子
        /// </summary>
        [Description("孩子")]
        public IEnumerable<Person> Sons { get; set; }

        
    }

    public class Person
    {
        [Description("孩子名字")]
        string name { get; set; }
        [Description("孩子年龄")]
        int age { get; set; }
    }
}
