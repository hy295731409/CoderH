using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._7
{
    class Program
    {
        static void Main(string[] args)
        {
            //TaskClass.GetTestRes();
            //Console.WriteLine("flag");
            

            Person person1 = new Person();

            string json1 = JsonConvert.SerializeObject(person1, Formatting.Indented);
            string json = JsonConvert.SerializeObject(person1);
            Console.WriteLine("--------包含属性的默认值与null序列化-------");
            Console.WriteLine(json1);


            Console.WriteLine("--------不包含属性的默认值序列化-------");

            Person person2 = new Person()
            {
                Name = "GongHui",
                Age = 28
            };

            string json2 = JsonConvert.SerializeObject(person2, Formatting.Indented, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
            Console.WriteLine(json2);

            Console.WriteLine("--------不包含属性的null序列化-------");

            string json3 = JsonConvert.SerializeObject(person2, Formatting.Indented, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            Console.WriteLine(json3);


            var s = "{\"Name\":null,\"Age\":0,\"Partner\":null,\"Salary\":0.0}";
            var p1 = JsonConvert.DeserializeObject<Person>(s);
            s = "{\"Name\":null,\"Age\":0,\"Partner\":null}";
            var p2 = JsonConvert.DeserializeObject<Person>(s);
            Console.ReadKey();
        }
    }
}
