namespace Calculator.Microservices.Client.Web.Services
{
    public class MessageBusService : IMessageBusService
    {
        private readonly MessageBus _messageBus;
        private bool _disposed;

        public MessageBusService()
        {
            _messageBus = new MessageBus(Environment.GetEnvironmentVariable("BOOTSTRAP_SERVERS") ?? "localhost:9093");
        }

        public void Send(string topic, string message)
        {
            _messageBus.SendMessage(topic, message);
        }

        public void SubscribeOn<T>(string topic, Action<T> action) where T : class
        {
            Task.Run(() => _messageBus.SubscribeOnTopic<T>(topic, action, CancellationToken.None));
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
                _messageBus?.Dispose();
            }

            _disposed = true;
        }
    }
}
