using Calculator.Microservices.Shared.Kafka.Library;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

using var messageBus = new MessageBus(Environment.GetEnvironmentVariable("BOOTSTRAP_SERVERS") ?? "localhost:9092");


app.Run();
