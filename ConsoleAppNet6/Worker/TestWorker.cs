using ConsoleAppNet6.Config;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppNet6.Worker
{
    public class TestWorker : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var db = new SqlServerContext();
            Console.WriteLine(db.Book.FirstOrDefault()?.ToString());
            return Task.CompletedTask;
        }
    }
}
