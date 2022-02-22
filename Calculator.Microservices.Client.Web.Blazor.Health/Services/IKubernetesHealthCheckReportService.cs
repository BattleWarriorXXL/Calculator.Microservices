using Calculator.Microservices.Shared.Library.HealthCheck;
using k8s.Models;

namespace Calculator.Microservices.Client.Web.Blazor.Health.Services
{
    public interface IKubernetesHealthCheckReportService
    {
        public Dictionary<string, List<V1Pod>> DiscoveredServices { get; set; }

        Task CheckAsync(CancellationToken cancellationToken);
        void Subscribe(Action<UIKubernetesHealthReport> onCheckHealth);
        void Unsubscribe(Action<UIKubernetesHealthReport> onCheckHealth);
    }
}
