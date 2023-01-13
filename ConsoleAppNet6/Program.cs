// See https://aka.ms/new-console-template for more information
using ConsoleAppNet6.Config;
using ConsoleAppNet6.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;

Console.WriteLine("Hello, World!");


IHostBuilder builder = Host.CreateDefaultBuilder(args);

//配置当前环境变量
#if DEBUG
builder.UseEnvironment("Development");
#elif STAGING
builder.UseEnvironment("Staging");
#endif
//判断当前系统是否为windows
if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
{
    builder.UseWindowsService((config) => config.ServiceName = "EM.NetCore.VoiceServicePlatform.Server");
}

IHost host = builder.ConfigureServices((hostContext,services) =>
{
    //services.AddDbContext<SqlServerContext>(options =>
    //{
    //    var connStr = hostContext.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
    //    options.UseSqlServer(connStr, opt =>
    //    {
    //        opt.CommandTimeout(10);
    //    });
    //});
    services.AddHostedService<TestWorker>();
}).Build();

await host.RunAsync();

////配置当前环境变量
//#if DEBUG
//builder.UseEnvironment("Development");
//#elif STAGING
//builder.UseEnvironment("Staging");
//#endif
////判断当前系统是否为windows
//if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
//{
//    builder.UseWindowsService((config) => config.ServiceName = "EM.NetCore.VoiceServicePlatform.Server");
//}
//IHost host = builder
//    //注入 Log4Net
//    .ConfigureLogging((configure) =>
//    {
//        configure.AddLog4Net();

//        //NEventSocket 需要显示提供 LoggerFactory        
//        NEventSocket.Logging.Logger.Configure(LoggerFactory.Create(x =>
//        {
//            x.AddLog4Net();
//        }));
//    })
//    .ConfigureServices(services =>
//    {
//        services.AddRabbitMQServer();
//        services.AddSingleton<SpeechServiceHelper>();
//        services.AddSingleton<SFServerHelper>();
//        services.AddSingleton<VoiceServiceDAL>();
//        services.AddSingleton<EmSftpClient>();

//        //测试代码
//        //services.AddSingleton<WorkerMain>();
//        //services.AddHostedService<TestWorker>();

//        //正式
//        services.AddHostedService<WorkerMain>();


//    })
//    .Build();

//await host.RunAsync();