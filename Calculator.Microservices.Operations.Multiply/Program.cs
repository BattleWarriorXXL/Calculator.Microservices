using Calculator.Microservices.Shared.Extensions;
using Calculator.Microservices.Operations.Multiply.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus("multiply");
builder.Services.AddTransient<MultiplyIntegrationEventHandler>();

var app = builder.Build();

app.Subscribe<MultiplyIntegrationEvent, MultiplyIntegrationEventHandler>();

app.Run();
