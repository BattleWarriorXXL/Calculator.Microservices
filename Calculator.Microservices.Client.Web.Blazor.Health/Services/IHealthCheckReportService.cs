using Calculator.Microservices.Shared.Library.HealthCheck;

namespace Calculator.Microservices.Client.Web.Blazor.Health.Services
{
    public interface IHealthCheckReportService
    {
        Task CheckAsync(CancellationToken cancellationToken);
        void Subscribe(Action<DomainUIHealthReport> onCheckHealth);
        void Unsubscribe(Action<DomainUIHealthReport> onCheckHealth);
    }
}
