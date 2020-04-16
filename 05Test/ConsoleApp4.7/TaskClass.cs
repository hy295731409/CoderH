using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4._7
{
    public class TaskClass
    {
        public static async Task<int> Test()
        {
            Task<int> task = new Task<int>(() =>
            {
                var res = 0;
                for (int i = 0; i < 1000; i++)
                {
                    Console.WriteLine($"current i = {i}");
                    res += i;
                    Thread.Sleep(10);
                }

                return res;
            });
            task.Start();

            return await task;
        }

        public static async void GetTestRes()
        {
            var res = await Test();
            Console.WriteLine(res);
        }
    }
}
