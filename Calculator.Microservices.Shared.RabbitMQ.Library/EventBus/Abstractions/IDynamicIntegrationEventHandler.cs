namespace Calculator.Microservices.Shared.RabbitMQ.Library.EventBus.Abstractions;

public interface IDynamicIntegrationEventHandler
{
    Task Handle(dynamic eventData);
}
