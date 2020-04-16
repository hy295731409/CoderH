using AccessWinform;
using RabbitMQClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JSHC.RabbitMQClient.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadDLLTest.ReadDll();
            //Send();
            //Console.Read();
        }

        private static void Send()
        {
            for (int i = 0; i < 5; i++)
            {
                //Thread.Sleep(400);
                string message = $" message{i}";
                RabbitMqClient.Instance.TriggerEventMessage(message, "MQ.Advice.Queue", "MQ.Advice.Queue");
                Console.WriteLine($"send : {message}");
                Thread.Sleep(100);
            }

            Receive();
        }

        private static void Receive()
        {
            RabbitMqClient.Instance.ActionEventMessage += mqClient_ActionEventMessage;
            RabbitMqClient.Instance.ListenInit("MQ.Advice.Queue", "MQ.Advice.Queue");
        }

        private static void mqClient_ActionEventMessage(EventMessageResult result)
        {

            result.IsOperationOk = true; //处理成功

            Console.WriteLine(result.MessageBody);
        }
        
    }
}
