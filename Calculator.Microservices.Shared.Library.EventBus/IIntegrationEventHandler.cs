namespace Calculator.Microservices.Shared.Library.EventBus
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(dynamic eventData);
    }

    public interface IIntegrationEventHandler { }
}
