namespace Calculator.Microservices.Client.Web.Blazor.Health.Library.Configuration
{
    public class Settings
    {
        public int EvaluationTimeInSeconds { get; set; } = 10;
        public List<HealthCheckSetting> HealthChecks { get; set; } = new List<HealthCheckSetting>();
    }

    public class HealthCheckSetting
    {
        public string Name { get; set; }
        public string Uri { get; set; }
    }
}
