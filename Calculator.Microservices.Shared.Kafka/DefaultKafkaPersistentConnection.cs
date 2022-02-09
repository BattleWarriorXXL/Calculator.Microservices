using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Net.Sockets;

namespace Calculator.Microservices.Shared.Kafka
{
    public class DefaultKafkaPersistentConnection : IKafkaPersistentConnection
    {
        private readonly ProducerBuilder<string, byte[]> _producerBuilder;
        private readonly ConsumerBuilder<string, byte[]> _consumerBuilder;
        private readonly ILogger<DefaultKafkaPersistentConnection> _logger;
        private readonly int _retryCount;

        private IConsumer<string, byte[]>? _consumer;

        private bool _disposed;

        object sync_root = new();

        public DefaultKafkaPersistentConnection(ProducerBuilder<string, byte[]> producerBuilder,
                                                ConsumerBuilder<string, byte[]> consumerBuilder,
                                                ILogger<DefaultKafkaPersistentConnection> logger,
                                                int retryCount = 5)
        {
            _producerBuilder = producerBuilder;
            _consumerBuilder = consumerBuilder;
            _logger = logger;
            _retryCount = retryCount;
        }

        public bool IsConnected => _consumer != null;

        public IProducer<string, byte[]> CreateProducer()
        {
            return _producerBuilder.Build();
        }

        public IConsumer<string, byte[]> GetConsumer()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No Kafka connections are available to perform this action");
            }

            return _consumer!;
        }

        public bool TryConnect()
        {
            _logger.LogInformation("Kafka Client is trying to connect");

            lock (sync_root)
            {
                var policy = RetryPolicy.Handle<SocketException>()
                                        .Or<KafkaException>()
                                        .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                                        {
                                            _logger.LogWarning(ex, "Kafka Client could not connect after {TimeOut}s ({ExceptionMessage})", $"{time.TotalSeconds:n1}", ex.Message);
                                        }
                                        );

                policy.Execute(() =>
                {
                    _consumer = _consumerBuilder.Build();
                });

                if (IsConnected)
                {
                    _logger.LogInformation("Kafka Client acquired a persistent connection");

                    return true;
                }
                else
                {
                    _logger.LogCritical("FATAL ERROR: Kafka connections could not be created and opened");

                    return false;
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
                _consumer?.Dispose();
            }

            _disposed = true;
        }
    }
}
