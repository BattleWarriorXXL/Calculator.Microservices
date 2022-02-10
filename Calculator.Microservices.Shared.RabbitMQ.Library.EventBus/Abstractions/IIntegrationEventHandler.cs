namespace Calculator.Microservices.Shared.RabbitMQ.Library.EventBus.Abstractions
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(dynamic eventData);
    }

    public interface IIntegrationEventHandler { }
}
