using Calculator.Microservices.Shared.Extensions;
using Calculator.Microservices.Operations.Add.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus("add");
builder.Services.AddTransient<AddIntegrationEventHandler>();

var app = builder.Build();

app.Subscribe<AddIntegrationEvent, AddIntegrationEventHandler>();

app.Run();
