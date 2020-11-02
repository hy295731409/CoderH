using AccessWinform;
using ConsoleApp.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ////协变：让ITest<Fruit> list = new Test<Apple>();这类代码成立！
            //ITest<Fruit> list = new Test<Apple>();
            //PrintFruit(list);

            //IEnumerable<Fruit> fruits = new List<Apple>() { new Apple() };
            ////List<Fruit> f = new List<Apple>();
            //Print(fruits);

            ////逆变：让ITest<Apple> test = new Test<Fruit>();这类代码成立！
            //ITest2<Apple> test = new Test2<Fruit>();

            //test.Set(new Apple());

            new StringMemoryResearch().StringTest();

            Console.WriteLine(default(indGroup));
       
            Console.ReadLine();
        }

        public static void PrintFruit(ITest<Fruit> test)
        {
            Console.WriteLine(test.Get().Name);
        }
        public static void Print(IEnumerable<Fruit> test)
        {
            test.ToList().ForEach(i => Console.WriteLine(i.Name));
        }
        private struct indGroup
        {
            public int id { get; set; }
            public string name { get; set; }
        }
    }

    public class Fruit
    {
        public string Name = "Fruit";
    }
    public class Apple : Fruit
    {
        public Apple() => this.Name = "Apple";
    }

    
}
