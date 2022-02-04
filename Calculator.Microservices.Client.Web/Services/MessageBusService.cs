namespace Calculator.Microservices.Client.Web.Services
{
    public class MessageBusService : IMessageBusService
    {
        private readonly MessageBus _messageBus;
        private bool _disposed;

        public MessageBusService()
        {
            _messageBus = new MessageBus();
        }

        public void Send(string topic, Message message)
        {
            _messageBus.SendMessage(topic, message);
        }

        public void SubscribeOn(string topic, Action<Message> action)
        {
            Task.Run(() => _messageBus.SubscribeOnTopic(topic, action, CancellationToken.None));
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
