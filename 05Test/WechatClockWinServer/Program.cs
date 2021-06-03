using System;
using Topshelf;

namespace WechatClockWinServer
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<WechatNotification>((s) =>
                {
                    s.ConstructUsing(name => new WechatNotification());
                    s.WhenStarted((t) => t.Start());
                    s.WhenStopped((t) => t.Stop());
                });

                x.RunAsLocalSystem();

                //服务的描述
                x.SetDescription("WechatNotification");
                //服务的显示名称
                x.SetDisplayName("打卡小助手");
                //服务名称
                x.SetServiceName("打卡小助手");

            });
        }
    }
}
