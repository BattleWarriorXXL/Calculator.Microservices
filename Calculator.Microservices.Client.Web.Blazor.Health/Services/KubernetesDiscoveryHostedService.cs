using Calculator.Microservices.Client.Web.Blazor.Health.Library.Configuration;
using k8s;
using k8s.Models;
using Microsoft.Extensions.Options;

namespace Calculator.Microservices.Client.Web.Blazor.Health.Services
{
    public class KubernetesDiscoveryHostedService : IHostedService
    {
        private readonly IKubernetesHealthCheckReportService _kubernetesHealthCheckReportService;
        private readonly IHostApplicationLifetime _hostLifetime;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<KubernetesDiscoveryHostedService> _logger; 
        private readonly KubernetesDiscoverySettings _kubernetesDiscoverySettings;

        private IKubernetes? _discoveryClient;

        private Task? _executingTask;

        public KubernetesDiscoveryHostedService(IKubernetesHealthCheckReportService kubernetesHealthCheckReportService,
                                                IHostApplicationLifetime hostLifetime, IServiceProvider serviceProvider,
                                                ILogger<KubernetesDiscoveryHostedService> logger,
                                                IOptions<KubernetesDiscoverySettings> kubernetesDiscoverySettings)
        {
            _kubernetesHealthCheckReportService = kubernetesHealthCheckReportService;
            _hostLifetime = hostLifetime;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _kubernetesDiscoverySettings = kubernetesDiscoverySettings.Value;
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
                if (_kubernetesDiscoverySettings.Enabled)
                {
                    try
                    {
                        var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
                        _discoveryClient = new Kubernetes(config);

                        await StartK8sService(cancellationToken);
                    }
                    catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
                    {

                    }
                }

            });

            return Task.CompletedTask;
        }

        private async Task StartK8sService(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var dicscoveredServices = new Dictionary<string, List<V1Pod>>();

                _logger.LogInformation("Starting kubernetes service discovery...");

                using var scope = _serviceProvider.CreateScope();

                try
                {
                    var services = await _discoveryClient.ListNamespacedServiceAsync(_kubernetesDiscoverySettings.Namespace);
                    var pods = await _discoveryClient.ListNamespacedPodAsync(_kubernetesDiscoverySettings.Namespace);

                    if (services != null)
                    {
                        foreach (var service in services.Items)
                        {
                            if (service.Spec.Type == "LoadBalancer")
                            {
                                dicscoveredServices.Add(service.Metadata.Name, pods.Items.Where(p => p.Metadata.Labels["app"] == service.Spec.Selector["app"]).ToList());
                            }
                        }
                    }

                    _kubernetesHealthCheckReportService.DiscoveredServices = dicscoveredServices;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred on kubernetes service discovery");
                }

                await Task.Delay(_kubernetesDiscoverySettings.RefreshTimeOnSeconds * 1000);
            }
        }
    }
}
