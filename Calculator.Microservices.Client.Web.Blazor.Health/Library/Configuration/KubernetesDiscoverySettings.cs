namespace Calculator.Microservices.Client.Web.Blazor.Health.Library.Configuration
{
    public class KubernetesDiscoverySettings
    {
        public bool Enabled { get; set; } = false;
        public string Namespace { get; set; } = "default";
        public int RefreshTimeOnSeconds { get; set; } = 60;
    }
}
