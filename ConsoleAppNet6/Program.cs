
using System.Runtime.InteropServices;
using Microsoft.Extensions.Hosting;
using ConsoleAppNet6.Worker;
using Microsoft.Extensions.DependencyInjection;

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


IHost host = builder.ConfigureLogging((configure) => { configure.AddLog4Net(); })
    .ConfigureServices((context, services) =>
    {
        

        ////1注册db
        //services.AddDbContextFactory<MysqlDbContext>(options =>
        //{
        //    var conn = context.Configuration.GetConnectionString("DefaultConnection");
        //    //var t2 = context.Configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        //    options.UseMySQL(conn)
        //           .UseLoggerFactory(LoggerFactory.Create(x => x.AddLog4Net().SetMinimumLevel(LogLevel.Warning)))
        //           .EnableDetailedErrors();
        //    //.ConfigureWarnings(b=>b.Log((RelationalEventId.ConnectionOpened, LogLevel.Debug), (RelationalEventId.ConnectionClosed, LogLevel.Debug)));修改日志级别
        //});

        //测试代码
        //services.AddSingleton<WorkerMain>();
        services.AddHostedService<TestWorker>();

        //正式
        //services.AddHostedService<WorkerMain>();


    })
    .Build();

await host.RunAsync();

