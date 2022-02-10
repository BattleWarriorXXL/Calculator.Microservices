using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Operations.Add.IntegrationEvents.Events
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
