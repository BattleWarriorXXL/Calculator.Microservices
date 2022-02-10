using Calculator.Microservices.Shared.Extensions;
using Calculator.Microservices.Operations.Subtract.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus("subtract");
builder.Services.AddTransient<SubtractIntegrationEventHandler>();

var app = builder.Build();

app.Subscribe<SubtractIntegrationEvent, SubtractIntegrationEventHandler>();

app.Run();
