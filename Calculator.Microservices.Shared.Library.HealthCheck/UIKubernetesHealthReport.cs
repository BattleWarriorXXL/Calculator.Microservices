namespace Calculator.Microservices.Shared.Library.HealthCheck
{
    public class UIKubernetesHealthReport
    {
        public string Name { get; set; }

        public KubernetesStatus Status { get; set; }

        public List<UIKubernetesHealthReportEntry> Entries { get; set; }
    }

    public class UIKubernetesHealthReportEntry
    {
        public string Name { get ; set; }

        public string PodStatus { get; set; }
    }

    public enum KubernetesStatus
    {
        Failed,
        Pending,
        Running
    }
}
