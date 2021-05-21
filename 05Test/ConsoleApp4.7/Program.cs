using ConsoleApp4._7.TaskDemo;
using ConsoleApp4._7.test;
using ConsoleApp4._7.WebServiceClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4._7
{
    class Program
    {
        static void Main(string[] args)
        {
            //TaskClass.GetTestRes();
            //Console.WriteLine("flag");
            //Console.WriteLine($"AthreadId=" + Thread.CurrentThread.ManagedThreadId);
            //test();

            //Console.WriteLine($"AthreadId=" + Thread.CurrentThread.ManagedThreadId);
            //Console.ReadKey();

            WebServiceTest.Test();

            Console.ReadKey();
        }

        private async static void test()
        {
            Console.WriteLine($"threadId=" + Thread.CurrentThread.ManagedThreadId);
            //只有await+task的时候才会新开线程并当前主线程推出方法，这里不会
            await Demo1.GetResAsync("ddd");
            Console.WriteLine($"threadId=" + Thread.CurrentThread.ManagedThreadId);
        }
    }
}
