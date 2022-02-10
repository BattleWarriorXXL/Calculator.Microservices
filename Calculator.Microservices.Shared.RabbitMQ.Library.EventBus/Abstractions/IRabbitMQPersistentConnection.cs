namespace Calculator.Microservices.Shared.RabbitMQ.Library.EventBus.Abstractions
{
    public interface IRabbitMQPersistentConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
        IModel CreateModel();
    }
}
