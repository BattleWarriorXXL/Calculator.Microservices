using Calculator.Microservices.Shared.Library;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Calculator.Microservices.Shared.Kafka
{
    public class EventBusKafka : IEventBus
    {
        private readonly IKafkaPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusKafka> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEventBusSubscriptionsManager _subscriptionsManager;
        private readonly string _target;
        private readonly int _retryCount;

        private IConsumer<string, byte[]> _consumer;

        private bool _disposed;

        public EventBusKafka(IKafkaPersistentConnection persistentConnection, ILogger<EventBusKafka> logger,
                             IServiceProvider serviceProvider, IEventBusSubscriptionsManager subscriptionsManager,
                             string target, int retryCount = 5)
        {
            _persistentConnection = persistentConnection;
            _logger = logger;
            _serviceProvider = serviceProvider;
            _subscriptionsManager = subscriptionsManager;
            _target = target;
            _retryCount = retryCount;
            
            _consumer = GetConsumer();
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            var policy = RetryPolicy.Handle<KafkaException>()
                                    .Or<SocketException>()
                                    .WaitAndRetry(_retryCount, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                                    {
                                        _logger.LogWarning(ex, "Could not publish event: {EventId} after {Timeout}s ({ExceptionMessage})", @event.Id, $"{time.TotalSeconds:n1}", ex.Message);
                                    });

            var eventName = @event.GetType().Name;

            _logger.LogTrace("Using Kafka producer to publish event: {EventId} ({EventName})", @event.Id, eventName);

            Task.Run(async () =>
            {
                using (var producer = _persistentConnection.CreateProducer())
                {
                    var body = JsonSerializer.SerializeToUtf8Bytes(@event, @event.GetType(), new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                    await policy.Execute(async () =>
                    {
                        _logger.LogTrace("Publishing event to Kafka: {EventId}", @event.Id);

                       await producer.ProduceAsync(@event.Target, new Message<string, byte[]> { Key = eventName, Value = body });
                    });
                }
            });
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = _subscriptionsManager.GetEventKey<T>();
            DoInternalSubscription(eventName);

            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetGenericTypeName());

            _subscriptionsManager.AddSubscription<T, TH>();
            Task.Run(() => StartBasicConsume());
        }

        private void DoInternalSubscription(string eventName)
        {
            var containsKey = _subscriptionsManager.HasSubscriptionsForEvent(eventName);
            if (!containsKey)
            {
                if (!_persistentConnection.IsConnected)
                {
                    _persistentConnection.TryConnect();
                }

                _consumer.Assign(new List<TopicPartitionOffset> { new TopicPartitionOffset(_target, 0, -1) });
            }
        }

        private async Task StartBasicConsume()
        {
            _logger.LogTrace("Starting Kafka basic consume");

            if (_consumer != null)
            {
                while (true)
                {
                    var result = _consumer.Consume();
                    if (result != null && result.Message.Key is string key && result.Message.Value is byte[] value)
                    {
                        var eventName = key;
                        var message = Encoding.UTF8.GetString(value);

                        await ProcessEvent(eventName, message);
                    }
                }
            }
            else
            {
                _logger.LogError("StartBasicConsume can't call on _consumer == null");
            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            _logger.LogTrace("Processing Kafka event: {EventName}", eventName);

            if (_subscriptionsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = _subscriptionsManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    var handler = _serviceProvider.GetService(subscription.HandlerType);
                    if (handler == null) continue;
                    var eventType = _subscriptionsManager.GetEventTypeByName(eventName);
                    var integrationEvent = JsonSerializer.Deserialize(message, eventType, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await Task.Yield();
                    if (concreteType?.GetMethod("Handle")?.Invoke(handler, new object[] { integrationEvent }) is Task task)
                    {
                        await task;
                    }
                }
            }
            else
            {
                _logger.LogWarning("No subscription for Kafka event: {EventName}", eventName);
            }
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private IConsumer<string, byte[]> GetConsumer()
        {
            if (!_persistentConnection.IsConnected)
            {
                _persistentConnection.TryConnect();
            }

            _logger.LogTrace("Creating Kafka consumer");

            var consumer = _persistentConnection.GetConsumer();
            return consumer;
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
                _subscriptionsManager.Clear();
            }

            _disposed = true;
        }
    }
}