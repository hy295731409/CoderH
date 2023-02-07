using ConsoleAppNet6.Config;
using ConsoleAppNet6.Logger;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet6.Worker
{
    public class TestWorker : BackgroundService
    {
        private readonly ILogger _logger;
        private static readonly IEventIDLog log = EventIDLogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TestWorker(ILogger logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //using var db = new SqlServerContext();
            //Console.WriteLine(db.Book.FirstOrDefault()?.ToString());
            _logger.LogInformation("zzzz");
            log.Info(1, "Application [" + System.Reflection.Assembly.GetEntryAssembly().GetName().Name + "] Start");

            log.Warn(40, "This is a warn message ");

            log.Info(2, "Application [" + System.Reflection.Assembly.GetEntryAssembly().GetName().Name + "] Stop");

            return Task.CompletedTask;
        }
    }
}
