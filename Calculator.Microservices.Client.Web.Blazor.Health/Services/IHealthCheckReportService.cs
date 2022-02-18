using Calculator.Microservices.Shared.Library.HealthCheck;

namespace Calculator.Microservices.Client.Web.Blazor.Health.Services
{
    public interface IHealthCheckReportService
    {
        Task CheckAsync(CancellationToken cancellationToken);
        void Subscribe(Action<UIHealthReport> onCheckHealth);
        void Unsubscribe(Action<UIHealthReport> onCheckHealth);
    }
}
