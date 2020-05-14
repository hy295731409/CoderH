using AccessWinform;
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
            //协变：让ITest<Fruit> list = new Test<Apple>();这类代码成立！
            ITest<Fruit> list = new Test<Apple>();
            PrintFruit(list);

            IEnumerable<Fruit> fruits = new List<Apple>() { new Apple() };
            Print(fruits);

            //逆变：让ITest<Apple> test = new Test<Fruit>();这类代码成立！
            ITest2<Apple> test = new Test2<Fruit>();
            test.Set(new Apple());

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
    }

    



}
