using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Shared.IntegrationEvents.Events
{
    public record DivideIntegrationEvent : IntegrationEvent
    {
        public double A { get; init; }
        public double B { get; init; }

        public DivideIntegrationEvent(double a, double b) : base("divide")
        {
            A = a;
            B = b;
        }
    }
}
