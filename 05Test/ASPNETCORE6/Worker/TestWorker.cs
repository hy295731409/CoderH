namespace ASPNETCORE6.Worker
{
    public class TestWorker : BackgroundService
    {
        private readonly ILogger<TestWorker> _logger;

        public TestWorker(ILogger<TestWorker> logger)
        {
            _logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("start");
            return Task.CompletedTask;
        }
    }
}
