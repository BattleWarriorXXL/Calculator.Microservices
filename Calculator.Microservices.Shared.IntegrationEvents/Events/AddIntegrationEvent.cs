using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Shared.IntegrationEvents.Events
{
    public record AddIntegrationEvent : IntegrationEvent
    {

        public double A { get; init; }
        public double B { get; init; }

        public AddIntegrationEvent(double a, double b)
        {
            A = a;
            B = b;
        }
    }
}
