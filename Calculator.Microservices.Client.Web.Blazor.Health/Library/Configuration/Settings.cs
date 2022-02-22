namespace Calculator.Microservices.Client.Web.Blazor.Health.Library.Configuration
{
    public class Settings
    {
        public int EvaluationTimeInSeconds { get; set; } = 10;
        public Dictionary<string, HealthCheckSetting> HealthChecks { get; set; } = new();
    }

    public class HealthCheckSetting
    {
        public string Name { get; set; }
        public string Uri { get; set; }
    }
}
