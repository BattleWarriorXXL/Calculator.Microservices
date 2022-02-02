namespace Calculator.Microservices.Client.Web.Services
{
    public interface IMessageBusService : IDisposable
    {
        void Send(string topic, string message);
        void SubscribeOn<T>(string topic, Action<T> action) where T : class;
    }
}
