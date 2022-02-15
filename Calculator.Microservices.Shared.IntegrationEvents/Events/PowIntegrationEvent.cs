using Calculator.Microservices.Shared.Library;

namespace Calculator.Microservices.Shared.IntegrationEvents.Events
{
    public record PowIntegrationEvent : IntegrationEvent
    {
        public double X { get; init; }
        public double Y { get; init; }

        public PowIntegrationEvent(double x, double y) : base("pow")
        {
            X = x;
            Y = y;
        }
    }
}
