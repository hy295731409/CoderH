using ConsoleApp4._7.TaskDemo;
using ConsoleApp4._7.test;
using ConsoleApp4._7.WebServiceClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp4._7
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //TaskClass.GetTestRes();
            //Console.WriteLine("flag");
            //Console.WriteLine($"AthreadId=" + Thread.CurrentThread.ManagedThreadId);
            //Demo1.test();
            Demo2.testAsync();
            Console.WriteLine("出来了");
            //Console.WriteLine($"AthreadId=" + Thread.CurrentThread.ManagedThreadId);
            //Console.ReadKey();

            //WebServiceTest.Test();

            //var test = new Test();
            //test.GetTest();

            //var demo = new XPath();
            //demo.demo();
            //new List<string>().GetType().GetTypeInfo().GetDeclaredMethod("MethodName").Invoke(obj, yourArgsHere);

            //List<Tuple<string,List<Person>>> tup = new List<Tuple<string, List<Person>>>();
            //tup.Add(new Tuple<string, List<Person>>("p1",new List<Person>() { new Person() { Name="p1"} }));
            //tup.Add(new Tuple<string, List<Person>>("p2",new List<Person>() { new Person() { Name="p2"} }));
            //tup.Add(new Tuple<string, List<Person>>("p3",new List<Person>() { new Person() { Name="p3"} }));
            //tup.Add(new Tuple<string, List<Person>>("p3.1",new List<Person>() { new Person() { Name="p3,1"} }));
            //var res = tup.SelectMany(s => s.Item2).ToList();

            Console.ReadKey();
        }
        
    }

}
