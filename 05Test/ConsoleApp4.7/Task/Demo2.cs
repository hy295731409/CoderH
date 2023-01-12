using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4._7.TaskDemo
{
    internal class Demo2
    {
        public static async Task testAsync()
        {
            Console.WriteLine("ThreadId1=" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Async proccess - enter Func1");
            await fun1Async();
            Console.WriteLine("proccess out fun1");

            Console.WriteLine("ThreadId2=" + Thread.CurrentThread.ManagedThreadId);

            Console.ReadKey();
        }

        static async Task fun1Async()
        {
            Console.WriteLine("ThreadId3=" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Func1 proccess - start");
            await fun2Async();
            Console.WriteLine("Func1 proccess - end");
            Console.WriteLine("ThreadId4=" + Thread.CurrentThread.ManagedThreadId);
        }

        static Task fun2Async()
        {
            Console.WriteLine("ThreadId5=" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Func2 proccess - start");
            Task.Delay(10000);
            return Task.CompletedTask;
        }
    }
}
