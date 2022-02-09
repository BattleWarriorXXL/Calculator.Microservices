namespace Calculator.Microservices.Shared.RabbitMQ.Library.EventBusRabbitMQ;

public interface IRabbitMQPersistentConnection
    : IDisposable
{
    bool IsConnected { get; }

    bool TryConnect();

    IModel CreateModel();
}
