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
            await fun2Async(); //加await的话，当前线程会等方法执行完成才走下一步,这个方法的后续都由新线程执行
            //fun2Async();         //不加waait，线程会继续往下执行，后续都有原线程执行
            Console.WriteLine("Func1 proccess - end");
            Console.WriteLine("ThreadId4=" + Thread.CurrentThread.ManagedThreadId);
        }

        static async Task fun2Async()
        {
            Console.WriteLine("ThreadId5=" + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Func2 proccess - start");
            await new TaskFactory().StartNew(() =>
            {
                Console.WriteLine("ThreadId6=" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(10000);
                Console.WriteLine("ThreadId7=" + Thread.CurrentThread.ManagedThreadId);
            });
            Console.WriteLine("Func2 proccess - end");
            Console.WriteLine("ThreadId8=" + Thread.CurrentThread.ManagedThreadId);
            //return Task.CompletedTask;
        }

        public static async Task fun3()
        {
            try
            {
                var taskFactory = new TaskFactory();
                var timeOut = TimeSpan.FromSeconds(5);
                using (var cancellationTokenSource = new CancellationTokenSource())
                {
                    var playTask = await taskFactory.StartNew(async () =>
                    {
                        await Task.Delay(1000 * 20);
                        Console.WriteLine("ok");
                    }, cancellationTokenSource.Token);
                    //var playTask = socket.Play(uuid, voiceFile);
                    //var delayTask = Task.Delay(timeOut, cancellationTokenSource.Token);
                    var completedTask = await Task.WhenAny(playTask, Task.Delay(timeOut, cancellationTokenSource.Token));
                    if (completedTask != playTask)
                    {
                        Console.WriteLine("任务超时了");
                        //谁先谁后？
                        //await socket.Hangup(uuid, HangupCause.CallRejected);
                        cancellationTokenSource.Cancel();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new TimeoutException("播放语音超时");
            }
        }
    }
}
