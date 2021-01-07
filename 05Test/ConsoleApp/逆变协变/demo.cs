using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.逆变协变
{
    public class demo
    {
        public void Test()
        {
            //协变：让ITest<Fruit> list = new Test<Apple>();这类代码成立！
            ITest<Fruit> list = new Test<Apple>();
            PrintFruit(list);

            IEnumerable<Fruit> fruits = new List<Apple>() { new Apple() };
            //List<Fruit> f = new List<Apple>();
            Print(fruits);

            //逆变：让ITest<Apple> test = new Test<Fruit>();这类代码成立！
            ITest2<Apple> test = new Test2<Fruit>();

            test.Set(new Apple());
        }
       

        public static void PrintFruit(ITest<Fruit> test)
        {
            Console.WriteLine(test.Get().Name);
        }
        public static void Print(IEnumerable<Fruit> test)
        {
            foreach (var item in test)
            {
                Console.WriteLine(item.Name);
            }
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
