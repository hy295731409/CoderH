using System;
using Topshelf;

namespace SocketDemo
{
    class Program
    {
        public static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.RunAsLocalSystem();
                x.SetDescription("Socket数据接收测试");
                x.SetDisplayName("Socket Data Recive service");
                x.SetServiceName("Socket Data Recive");
                x.Service(factory =>
                {
                    Server server = new Server();
                    return server;
                });
            });
        }
    }
}
