namespace Calculator.Microservices.Client.Web.Blazor.Health.Library.Configuration
{
    public class KubernetesHealthCheckSettings
    {
        public int EvaluationTimeInSeconds { get; set; } = 10;
    }
}
