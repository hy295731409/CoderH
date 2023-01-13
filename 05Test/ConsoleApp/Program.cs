using ConsoleApp.Grpc;
using ConsoleApp.发送socket;
using ConsoleApp.排序;
using ConsoleApp.算法;
using Grpc.Net.Client;
using Medicom.PASSPA2CollectService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region MyRegion

            //new StringMemoryResearch().StringTest();

            //var obj = new StructClassResearch();
            //obj.UsingClass();
            //obj.UsingStruct();


            //GrpcClient.CallAsync();

            ////需要监听的IP地址
            //string ip = "172.18.5.122";
            ////需要监听的I端口
            //int port = 5556;
            ////var socket = new SocketManager(10, 1024 * 1024);
            //////socket.ReceiveClientData += ReceiveMessage;
            ////socket.Init();
            ////socket.Start(new IPEndPoint(IPAddress.Parse(ip), port));
            //////logger.InfoFormat("正在监听{0}:{1}......", ip, port);
            ////socket.SendMessage()

            //string result = SendSocketServer.SocketSendReceive(ip, port);
            //Console.WriteLine(result);

            #endregion

            //LRU
            //LRUCache cache = new LRUCache(2);
            //cache.Put(1, 1);
            //cache.Put(2, 2);
            //var res = cache.Get(1);
            //cache.Put(3, 3);
            //res = cache.Get(2);
            //cache.Put(4, 4);
            //res = cache.Get(1);
            //res = cache.Get(2);
            //res = cache.Get(3);
            //res = cache.Get(4);

            //LoopQueue<string> queue = new LoopQueue<string>(5);
            //queue.Add("a");
            //queue.Add("b");
            //queue.Add("c");
            //queue.Add("d");
            //queue.Add("e");//full
            //queue.Remove();
            //queue.Remove();
            //queue.Remove();
            //queue.Add("e");

            // MinWindow.MINSTRING();
            var arr = new[] { 5, 3, 4, 9, 2, 10, 6 };
            //arr.GetType().GetTypeInfo().GetDeclaredMethod("MethodName").Invoke(obj, yourArgsHere);
           // SortDemo.qs(arr, 0, 6);

            Console.ReadLine();
        }

        

    }

   

    
}
