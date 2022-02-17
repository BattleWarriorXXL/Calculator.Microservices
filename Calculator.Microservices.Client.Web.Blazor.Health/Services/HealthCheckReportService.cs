using Calculator.Microservices.Client.Web.Blazor.Health.Extensions;
using Calculator.Microservices.Client.Web.Blazor.Health.Library.Configuration;
using Calculator.Microservices.Shared.Library.HealthCheck;
using Microsoft.Extensions.Options;

namespace Calculator.Microservices.Client.Web.Blazor.Health.Services
{
    public class HealthCheckReportService : IHealthCheckReportService
    {
        private readonly HttpClient _httpClient;
        private readonly Settings _settings;
        private readonly ILogger<HealthCheckReportService> _logger;

        private event Action<DomainUIHealthReport>? Notify;

        public HealthCheckReportService(IHttpClientFactory httpClientFactory,
                                        IOptions<Settings> settings,
                                        ILogger<HealthCheckReportService> logger)
        {
            _httpClient = httpClientFactory.CreateClient("health_checks");
            _settings = settings.Value;
            _logger = logger;

        }
        public async Task CheckAsync(CancellationToken cancellationToken)
        {
            var healthChecks = _settings.HealthChecks;

            foreach (var healthCheckSetting in healthChecks)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _logger.LogInformation("HealthReportService has been cancelled.");
                    break;
                }

                var healthCheckReport = await GetHealthReportAsync(healthCheckSetting);
                var domainHealthCheckReport = DomainUIHealthReport.CreateFrom(healthCheckReport);
                Notify?.Invoke(domainHealthCheckReport);
            }

            _logger.LogInformation("HealthReportService has completed.");
        }

        public async Task<UIHealthReport> GetHealthReportAsync(HealthCheckSetting setting)
        {
            try
            {
                var response = await _httpClient.GetAsync(setting.Uri);

                return await response.As<UIHealthReport>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GetHealthReport threw an exception when trying to get report from {setting.Uri} configured with name {setting.Name}.");
                return UIHealthReport.CreateFrom(ex);
            }
        }

        public void Subscribe(Action<DomainUIHealthReport> onCheckHealth)
        {
            Notify += onCheckHealth;
        }

        public void Unsubscribe(Action<DomainUIHealthReport> onCheckHealth)
        {
            Notify -= onCheckHealth;
        }
    }
}
