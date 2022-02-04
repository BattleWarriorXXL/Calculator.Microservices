namespace Calculator.Microservices.Client.Web.Services
{
    public interface IMessageBusService : IDisposable
    {
        void Send(string topic, Message message);
        void SubscribeOn(string topic, Action<Message> action);
    }
}
