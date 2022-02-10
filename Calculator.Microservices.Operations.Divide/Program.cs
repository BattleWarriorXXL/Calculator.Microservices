using Calculator.Microservices.Shared.Extensions;
using Calculator.Microservices.Operations.Divide.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus("divide");
builder.Services.AddTransient<DivideIntegrationEventHandler>();

var app = builder.Build();

app.Subscribe<DivideIntegrationEvent, DivideIntegrationEventHandler>();

app.Run();
