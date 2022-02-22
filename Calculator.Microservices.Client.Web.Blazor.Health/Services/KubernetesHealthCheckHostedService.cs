using Calculator.Microservices.Client.Web.Blazor.Health.Library.Configuration;
using Microsoft.Extensions.Options;

namespace Calculator.Microservices.Client.Web.Blazor.Health.Services
{
    public class KubernetesHealthCheckHostedService : IHostedService
    {
        private readonly IHostApplicationLifetime _hostLifetime;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<KubernetesHealthCheckHostedService> _logger;
        private readonly KubernetesHealthCheckSettings _settings;

        private Task? _executingTask;

        public KubernetesHealthCheckHostedService(IHostApplicationLifetime hostLifetime, IServiceProvider serviceProvider,
                                                  ILogger<KubernetesHealthCheckHostedService> logger,
                                                  IOptions<KubernetesHealthCheckSettings> settings)
        {
            _hostLifetime = hostLifetime;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _settings = settings.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _executingTask = ExecuteAsync(cancellationToken);

            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAll(_executingTask!, Task.Delay(Timeout.Infinite, cancellationToken));
        }

        private Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _hostLifetime.ApplicationStarted.Register(async () =>
            {
                try
                {
                    await CheckHealth(cancellationToken);
                }
                catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("KubernetesHealthCheckHostedService stopped.");
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
                        var runner = scope.ServiceProvider.GetRequiredService<IKubernetesHealthCheckReportService>();
                        await runner.CheckAsync(cancellationToken);

                        _logger.LogInformation("KubernetesHealthCheck collector HostedService executed successfully.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "KubernetesHealthCheck collector HostedService threw an error: {Error}", ex.Message);
                    }
                }

                await Task.Delay(_settings.EvaluationTimeInSeconds * 1000, cancellationToken);
            }
        }
    }
}
