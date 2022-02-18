using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Calculator.Microservices.Shared.Library.HealthCheck
{
    public class UIHealthReport
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public HealthStatus Status { get; set; }
        public TimeSpan TotalDuration { get; set; }
        public Dictionary<string, UIHealthReportEntry> Entries { get; }

        public UIHealthReport(Dictionary<string, UIHealthReportEntry> entries, TimeSpan totalDuration)
        {
            Entries = entries;
            TotalDuration = totalDuration;
        }

        public static UIHealthReport CreateFrom(HealthReport report)
        {
            var uiReport = new UIHealthReport(new Dictionary<string, UIHealthReportEntry>(), report.TotalDuration)
            {
                Status = report.Status,
            };

            foreach (var item in report.Entries)
            {
                var entry = new UIHealthReportEntry
                {
                    Data = item.Value.Data,
                    Description = item.Value.Description ?? string.Empty,
                    Duration = item.Value.Duration,
                    Status = item.Value.Status
                };

                if (item.Value.Exception != null)
                {
                    var message = item.Value.Exception.Message.ToString();

                    entry.Exception = message;
                    entry.Description = item.Value.Description ?? message;
                }

                entry.Tags = item.Value.Tags;

                uiReport.Entries.Add(item.Key, entry);
            }

            return uiReport;
        }

        public static UIHealthReport CreateFrom(Exception exception, string name, string uri, string entryName = "Endpoint")
        {
            var uiReport = new UIHealthReport(new Dictionary<string, UIHealthReportEntry>(), TimeSpan.FromSeconds(0))
            {
                Name = name,
                Uri = uri,
                Status = HealthStatus.Unhealthy,
            };

            uiReport.Entries.Add(entryName, new UIHealthReportEntry
            {
                Exception = exception.Message,
                Description = exception.Message,
                Duration = TimeSpan.FromSeconds(0),
                Status = HealthStatus.Unhealthy
            });

            return uiReport;
        }
    }

    public class UIHealthReportEntry
    {
        public IReadOnlyDictionary<string, object> Data { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public string Exception { get; set; }
        public HealthStatus Status { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}
