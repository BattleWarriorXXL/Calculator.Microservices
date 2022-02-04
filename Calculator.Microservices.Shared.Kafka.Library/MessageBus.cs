using Confluent.Kafka;

namespace Calculator.Microservices.Shared.Kafka.Library
{
    public class MessageBus : IDisposable
    {
        private readonly IProducer<Null, Message> _producer;
        private IConsumer<Ignore, Message>? _consumer;

        private readonly ProducerConfig _producerConfig;
        private readonly ConsumerConfig _cosumerConfig;

        private readonly string _sessionKey;

        private bool _disposed;

        public MessageBus() : this(Environment.GetEnvironmentVariable("BOOTSTRAP_SERVERS") ?? "localhost:19092") { }

        public MessageBus(string host)
        {
            _producerConfig = new ProducerConfig
            {
                BootstrapServers = host
            };

            _cosumerConfig = new ConsumerConfig
            {
                BootstrapServers = host,
                GroupId = "custom-group"
            };

            _producer = new ProducerBuilder<Null, Message>(_producerConfig).SetValueSerializer(new Message()).Build();

            _sessionKey = Guid.NewGuid().ToString();
        }

        public void SendMessage(string topic, Message message)
        {
            if (string.IsNullOrEmpty(message.Key))
            {
                message.Key = _sessionKey;
            }
            _producer.ProduceAsync(topic, new Message<Null, Message> { Value = message });
        }

        public void SubscribeOnTopic(string topic, Action<Message> action, CancellationToken cancellationToken)
        {
            using (_consumer = new ConsumerBuilder<Ignore, Message>(_cosumerConfig).SetValueDeserializer(new Message()).Build())
            {
                _consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topic, 0, -1) });

                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(cancellationToken);
                    if (result != null && result.Message.Value is Message value)
                    {
                        if (topic == Topics.Topics.RESULT_TOPIC && result.Message.Value.Key == _sessionKey)
                        {
                            action(value);
                        }
                        else if (topic == Topics.Topics.ACTION_TOPIC)
                        {
                            action(value);
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _producer?.Dispose();
                _consumer?.Dispose();
            }

            _disposed = true;
        }
    }
}