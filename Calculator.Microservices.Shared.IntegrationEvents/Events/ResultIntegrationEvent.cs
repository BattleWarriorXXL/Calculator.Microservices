using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Shared.IntegrationEvents.Events
{
    public record ResultIntegrationEvent : IntegrationEvent
    {
        public double C { get; init; }

        public ResultIntegrationEvent(double c)
        {
            C = c;
        }
    }
}
