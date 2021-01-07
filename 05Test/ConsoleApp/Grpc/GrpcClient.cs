using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Grpc
{
    public class GrpcClient
    {
        public static async void CallAsync()
        {
            Console.WriteLine("Hello gRPC!");
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var greeterClient = new Greeter.GreeterClient(channel);

            //https://docs.microsoft.com/zh-cn/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.0&WT.mc_id=DT-MVP-5003133
            var reply = await greeterClient.SayHelloAsync(new HelloRequest
            {
                Name = "Garfield"
            });
            Console.WriteLine("Greeter 服务返回数据: " + reply.Message);
            //var counterClient = new Count.CounterClient(channel);
            //// This switch must be set before creating the GrpcChannel/HttpClient.
            //AppContext.SetSwitch(
            //    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            //// The port number(5000) must match the port of the gRPC server.
            //var channel = GrpcChannel.ForAddress("http://localhost:5000");
            //var client = new Greet.GreeterClient(channel);
        }
    }
}
