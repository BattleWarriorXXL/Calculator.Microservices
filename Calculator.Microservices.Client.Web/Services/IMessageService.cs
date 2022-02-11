namespace Calculator.Microservices.Client.Web.Services
{
    public interface IMessageService
    {
        public void Subscribe(Action<object> action);
        public void Unsubscribe(Action<object> action);
        public void Notify(object message);
    }
}
