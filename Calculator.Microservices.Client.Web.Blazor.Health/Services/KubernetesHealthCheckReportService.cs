using Calculator.Microservices.Shared.Library.HealthCheck;
using k8s.Models;

namespace Calculator.Microservices.Client.Web.Blazor.Health.Services
{
    public class KubernetesHealthCheckReportService : IKubernetesHealthCheckReportService
    {
        private event Action<UIKubernetesHealthReport>? Notify;

        private Dictionary<string, List<V1Pod>> _discoveredServices = new();

        public Dictionary<string, List<V1Pod>> DiscoveredServices
        {
            get
            {
                return _discoveredServices;
            }
            set
            {
                _discoveredServices = value;
            }
        }

        public Task CheckAsync(CancellationToken cancellationToken)
        {
            var reports = new List<UIKubernetesHealthReport>();

            foreach (var discoveredServices in _discoveredServices)
            {
                reports.Add(new UIKubernetesHealthReport
                {
                    Name = discoveredServices.Key,
                    Entries = discoveredServices.Value.Select(x => new UIKubernetesHealthReportEntry
                    {
                        Name = x.Metadata.Name,
                        PodStatus = x.Status.Phase,
                    }).ToList()
                });
            }

            foreach (var report in reports)
            {
                if (report.Entries.All(x => x.PodStatus == "Running"))
                {
                    report.Status = KubernetesStatus.Running;
                }
                else if (report.Entries.Any(x => x.PodStatus == "Failed"))
                {
                    report.Status = KubernetesStatus.Failed;
                }
                else
                {
                    report.Status = KubernetesStatus.Pending;
                }

                Notify?.Invoke(report);
            }

            return Task.CompletedTask;
        }

        public void Subscribe(Action<UIKubernetesHealthReport> onCheckHealth)
        {
            Notify += onCheckHealth;
        }

        public void Unsubscribe(Action<UIKubernetesHealthReport> onCheckHealth)
        {
            Notify -= onCheckHealth;
        }
    }
}
