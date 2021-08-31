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
            //Demo1.test();

            //Console.WriteLine($"AthreadId=" + Thread.CurrentThread.ManagedThreadId);
            //Console.ReadKey();

            //WebServiceTest.Test();

            //var test = new Test();
            //test.GetTest();

            var demo = new XPath();
            demo.demo();
            Console.ReadKey();
        }

    }

    
}
