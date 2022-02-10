using Calculator.Microservices.Shared.Library;
using RabbitMQ.Client;

namespace Calculator.Microservices.Shared.RabbitMQ
{
    public interface IRabbitMQPersistentConnection : IPersistentConnection, IDisposable
    {
        IModel CreateModel();
    }
}
