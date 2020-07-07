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
        public async static void GetResAsync(string str)
        {
            Console.WriteLine($"threadId=" + Thread.CurrentThread.ManagedThreadId);
            await Task.Run(() => Todo());
        }
        private static void Todo()
        {
            Console.WriteLine($"todo threadId=" + Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(500);
        }
    }
}
