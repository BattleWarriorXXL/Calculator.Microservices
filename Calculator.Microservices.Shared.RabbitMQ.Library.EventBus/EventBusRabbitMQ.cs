namespace Calculator.Microservices.Shared.RabbitMQ.Library.EventBus
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        const string BROKER_NAME = "calculator_event_bus";

        private readonly IRabbitMQPersistentConnection _persistentConnection;
        private readonly ILogger<EventBusRabbitMQ> _logger;
        private readonly

        private bool _disposed;

        public void Publish(IntegrationEvent @event)
        {
            throw new NotImplementedException();
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            throw new NotImplementedException();
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
