using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Shared.IntegrationEvents.Events
{
    public record SubtractIntegrationEvent : IntegrationEvent
    {
        public double A { get; init; }
        public double B { get; init; }

        public SubtractIntegrationEvent(double a, double b) : base("subtract")
        {
            A = a;
            B = b;
        }
    }
}
