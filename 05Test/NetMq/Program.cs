using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace NetMqDemo
{
    class Program
    {
        protected static string address = "tcp://172.18.6.105";
        static void Main(string[] args)
        {
            //ReqRep();
            //PubSub(new string[] { "All"});
            //PubSub(new string[] { "TopicA" });
            //PubSub(new string[] { "TopicB" });

            XSub();

            //PushPull();

            Console.ReadKey();
        }

        /// <summary>
        /// 三经典模式：请求响应
        /// </summary>
        static void ReqRep()
        {
            using (var rep = new ResponseSocket())
            using (var req = new RequestSocket())
            {
                var port = rep.BindRandomPort(address);
                req.Connect(address + ":" + port);

                req.SendFrame("Hi");

                Console.WriteLine(rep.ReceiveMultipartStrings().FirstOrDefault());

                rep.SendFrame("Hi2");

                Console.WriteLine(req.ReceiveMultipartStrings().FirstOrDefault());
            }
        }

        /// <summary>
        /// 三经典模式：发布订阅
        /// 消息可以被每个订阅者消费
        /// </summary>
        static void PubSub(string[] args)
        {
            IList<string> allowableCommandLineArgs = new[] { "TopicA", "TopicB", "All" };
            var a1 = $"{address}:5556";
            var a2 = $"{address}:5557";
            if (args.Length != 1 || !allowableCommandLineArgs.Contains(args[0]))
            {
                Console.WriteLine("Expected one argument, either " +
                                  "'TopicA', 'TopicB' or 'All'");
                Environment.Exit(-1);
            }
            string topic = args[0] == "All" ? "" : args[0];
            Console.WriteLine("Subscriber started for Topic : {0}", topic);
            using (var subSocket = new SubscriberSocket())
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Connect(a1);
                subSocket.Subscribe(topic);
                Console.WriteLine("Subscriber socket connecting...");
                while (true)
                {
                    string messageTopicReceived = subSocket.ReceiveFrameString();
                    string messageReceived = subSocket.ReceiveFrameString();
                    Console.WriteLine(messageReceived);
                }
            }
        }


        static void XSub()
        {
            var a3 = $"{address}:55508";
            string topic = "TopicA"; // one of "TopicA" or "TopicB"
            using (var subSocket = new SubscriberSocket(a3))
            {
                subSocket.Options.ReceiveHighWatermark = 1000;
                subSocket.Subscribe(topic);
                Console.WriteLine("Subscriber socket connecting...");
                while (true)
                {
                    string messageTopicReceived = subSocket.ReceiveFrameString();
                    string messageReceived = subSocket.ReceiveFrameString();
                    Console.WriteLine(messageReceived);
                }
            }
        }

        /// <summary>
        /// 三经典模式：推拉模式(管道模式)
        /// 一个消息只能被消费一次
        /// </summary>
        static void PushPull()
        {
            using (var pullSocket = new PullSocket())
            using (var pushSocket = new PushSocket())
            {
                var port = pullSocket.BindRandomPort($"{address}");
                pushSocket.Connect($"{address}:" + port);

                pushSocket.SendMoreFrame("hello").SendFrame("hello world");
                var s = pullSocket.ReceiveFrameString();
                s += pullSocket.ReceiveFrameString();
                Console.WriteLine(s);//hellohello world
            }
        }
    }
}