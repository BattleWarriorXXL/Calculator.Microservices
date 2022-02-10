using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Shared.IntegrationEvents.Events
{
    public record MultiplyIntegrationEvent : IntegrationEvent
    {
        public double A { get; init; }
        public double B { get; init; }

        public MultiplyIntegrationEvent(double a, double b) : base("multiply")
        {
            A = a;
            B = b;
        }
    }
}
