namespace Calculator.Microservices.Client.Web.Services
{
    public class MessageService : IMessageService
    {
        private event Action<object>? NotifyEvent;

        public void Notify(object message)
        {
            if (message != null)
            {
                NotifyEvent?.Invoke(message);
            }
        }

        public void Subscribe(Action<object> action)
        {
            NotifyEvent += action;
        }

        public void Unsubscribe(Action<object> action)
        {
            NotifyEvent -= action;
        }
    }
}
