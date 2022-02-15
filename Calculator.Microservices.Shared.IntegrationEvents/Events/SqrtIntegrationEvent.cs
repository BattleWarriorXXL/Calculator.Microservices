using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Shared.IntegrationEvents.Events
{
    public record SqrtIntegrationEvent : IntegrationEvent
    {
        public double D { get; init; }

        public SqrtIntegrationEvent(double d) : base("sqrt")
        {
            D = d;
        }
    }
}
