﻿using Confluent.Kafka;

namespace Calculator.Microservices.Shared.Kafka.Library
{
    public class MessageBus : IDisposable
    {
        private readonly IProducer<Null, string> _producer;
        private IConsumer<Null, string>? _consumer;

        private readonly ProducerConfig _producerConfig;
        private readonly ConsumerConfig _cosumerConfig;

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

            _producer = new ProducerBuilder<Null, string>(_producerConfig).Build();
        }

        public void SendMessage(string topic, string message)
        {
            _producer.ProduceAsync(topic, new Message<Null, string> { Value = $"{message} " });
        }

        public void SubscribeOnTopic<T>(string topic, Action<T> action, CancellationToken cancellationToken) where T : class
        {
            using (_consumer = new ConsumerBuilder<Null, string>(_cosumerConfig).Build())
            {
                _consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(topic, 0, -1) });

                while (!cancellationToken.IsCancellationRequested)
                {
                    var result = _consumer.Consume(cancellationToken);
                    if (result != null && result.Message.Value is T value)
                    {
                        action(value);
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