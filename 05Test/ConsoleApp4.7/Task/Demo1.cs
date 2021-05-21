using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4._7.TaskDemo
{
    public class Demo1
    {
        public async static Task GetResAsync(string str)
        {
            Console.WriteLine($"threadId=" + Thread.CurrentThread.ManagedThreadId);//1
            await Task.Run(() => Todo());
        }
        private static void Todo()
        {
            Console.WriteLine($"todo threadId=" + Thread.CurrentThread.ManagedThreadId);//3
            Thread.Sleep(500);
        }


        public static void test2()
        {
            var task1 = new Task(() =>
            {
                Console.WriteLine("Begin");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Finish");
            });
            Console.WriteLine("Before start:" + task1.Status);
            task1.Start();
            Console.WriteLine("After start:" + task1.Status);
            task1.Wait();
            Console.WriteLine("After Finish:" + task1.Status);

            Console.Read();
        }
    }
}
