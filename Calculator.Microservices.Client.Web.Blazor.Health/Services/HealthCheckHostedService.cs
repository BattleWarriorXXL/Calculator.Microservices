using Calculator.Microservices.Client.Web.Blazor.Health.Library.Configuration;
using Microsoft.Extensions.Options;

namespace Calculator.Microservices.Client.Web.Blazor.Health.Services
{
    public class HealthCheckHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _lifetime;
        private readonly Settings _settings;
        private readonly ILogger<HealthCheckHostedService> _logger;

        private Task? _executingTask;
        private CancellationTokenSource _cancellationTokenSource;

        public HealthCheckHostedService(IServiceProvider serviceProvider,
                                        IHostApplicationLifetime lifetime,
                                        IOptions<Settings> settings,
                                        IConfiguration configuration,
                                        ILogger<HealthCheckHostedService> logger)
        {
            _serviceProvider = serviceProvider;
            _lifetime = lifetime;
            _settings = settings.Value;
            _logger = logger;

            _cancellationTokenSource = new CancellationTokenSource();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(_cancellationTokenSource.Token);

            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource.Cancel();
            await Task.WhenAll(_executingTask!, Task.Delay(Timeout.Infinite, cancellationToken));
        }

        private Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _lifetime.ApplicationStarted.Register(async () =>
            {
                try
                {
                    await CheckHealth(cancellationToken);
                }
                catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("HealthCheckHostedService stopped.");
                }
            });

            return Task.CompletedTask;
        }

        private async Task CheckHealth(CancellationToken cancellationToken)
        {
            var scopeFactory = _serviceProvider.GetRequiredService<IServiceScopeFactory>();

            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Checking health at {DateTime.UtcNow}");

                using (var scope = scopeFactory.CreateScope())
                {
                    try
                    {
                        var runner = scope.ServiceProvider.GetRequiredService<IHealthCheckReportService>();
                        await runner.CheckAsync(cancellationToken);

                        _logger.LogInformation("HealthCheck collector HostedService executed successfully.");
                    }
                    catch(Exception ex)
                    {
                        _logger.LogError(ex, "HealthCheck collector HostedService threw an error: {Error}", ex.Message);
                    }
                }

                await Task.Delay(_settings.EvaluationTimeInSeconds * 1000, cancellationToken);
            }
        }
    }
}
