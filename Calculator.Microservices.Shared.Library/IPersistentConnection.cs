namespace Calculator.Microservices.Shared.Library
{
    public interface IPersistentConnection : IDisposable
    {
        bool IsConnected { get; }
        bool TryConnect();
    }
}
