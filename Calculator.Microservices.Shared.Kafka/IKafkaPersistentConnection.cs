using Calculator.Microservices.Shared.Library;
using Confluent.Kafka;

namespace Calculator.Microservices.Shared.Kafka
{
    public interface IKafkaPersistentConnection : IPersistentConnection
    {
        public IProducer<string, byte[]> CreateProducer();
        public IConsumer<string, byte[]> GetConsumer();
    }
}
