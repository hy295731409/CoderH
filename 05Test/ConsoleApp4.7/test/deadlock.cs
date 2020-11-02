using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4._7.test
{
    public class deadlock
    {
        private static readonly object Lock = new object();

        public static void Test()
        {
            lock (Lock)
            {
                Task.Run(action: TestMethod1).Wait();
            }
        }

        private static void TestMethod1()
        {
            lock (Lock)
            {
                Console.WriteLine("xxx");
            }
        }
    }
}
