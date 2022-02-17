using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Calculator.Microservices.Shared.Library.HealthCheck
{
    public class DomainUIHealthReport
    {
        public string Name { get; private init; }
        public HealthStatus Status { get; private init; }
        public TimeSpan TotalDuration { get; set; }
        public Dictionary<string, UIHealthReportEntry> Entries { get; private init; }

        public DomainUIHealthReport(string name, HealthStatus status, TimeSpan totalDuration, Dictionary<string, UIHealthReportEntry> entries)
        {
            Name = name;
            Status = status;
            TotalDuration = totalDuration;
            Entries = entries;
        }

        public static DomainUIHealthReport CreateFrom(UIHealthReport report)
        {
            var domainServiceName = report.Entries.FirstOrDefault(e => e.Key.Contains("self_")).Key ?? "unknown";

            return new DomainUIHealthReport(domainServiceName, report.Status, report.TotalDuration, report.Entries);
        }
    }
}
