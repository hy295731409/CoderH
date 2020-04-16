using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;

namespace NetMqDemo2
{
    class Program
    {
        protected static string address = "tcp://172.18.6.105";
        static void Main(string[] args)
        {
            //Pub();
            Task.Factory.StartNew(() =>
            {
                XPub();
            });
            Task.Factory.StartNew(() =>
            {
                Intermediary();
            });

            Console.ReadKey();
        }

        /// <summary>
        /// 三经典模式：发布订阅
        /// 消息可以被每个订阅者消费，但是只能有一个发布者
        /// </summary>
        static void Pub()
        {
            var a1 = $"{address}:5556";
            
            Random rand = new Random(50);
            using (var pubSocket = new PublisherSocket())
            {
                Console.WriteLine("Publisher socket binding...");
                pubSocket.Options.SendHighWatermark = 1000;
                pubSocket.Bind(a1);
                for (var i = 0; i < 50; i++)
                {
                    var randomizedTopic = rand.NextDouble();
                    if (randomizedTopic > 0.5)
                    {
                        var msg = "TopicA msg-" + i;
                        Console.WriteLine("Sending message : {0}", msg);
                        pubSocket.SendMoreFrame("TopicA").SendFrame(msg);
                    }
                    else
                    {
                        var msg = "TopicB msg-" + i;
                        Console.WriteLine("Sending message : {0}", msg);
                        pubSocket.SendMoreFrame("TopicB").SendFrame(msg);
                    }
                    Thread.Sleep(500);
                }
            }
        }

        static void XPub()
        {
            var a2 = $"{address}:55507";
            using (var pubSocket = new PublisherSocket(a2))
            {
                Console.WriteLine("Publisher socket connecting...");
                pubSocket.Options.SendHighWatermark = 1000;
                var rand = new Random(50);

                while (true)
                {
                    Thread.Sleep(1000);
                    var randomizedTopic = rand.NextDouble();
                    if (randomizedTopic > 0.5)
                    {
                        var msg = "TopicA msg-" + randomizedTopic;
                        Console.WriteLine("Sending message : {0}", msg);
                        pubSocket.SendMoreFrame("TopicA").SendFrame(msg);
                    }
                    else
                    {
                        var msg = "TopicB msg-" + randomizedTopic;
                        Console.WriteLine("Sending message : {0}", msg);
                        pubSocket.SendMoreFrame("TopicB").SendFrame(msg);
                    }
                }
            }
        }

        static void Intermediary()
        {

            var a2 = $"{address}:55507";
            var a3 = $"{address}:55508";
            using (var xpubSocket = new XPublisherSocket(a3))
            using (var xsubSocket = new XSubscriberSocket(a2))
            {
                Console.WriteLine("Intermediary started, and waiting for messages");
                // proxy messages between frontend / backend
                var proxy = new Proxy(xsubSocket, xpubSocket);
                // blocks indefinitely
                proxy.Start();
            }
        }
    }
}