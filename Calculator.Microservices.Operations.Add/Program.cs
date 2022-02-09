using Calculator.Microservices.Shared.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus("add");
builder.Services.AddTransient<AddIntegrationEventHandler>();

var app = builder.Build();

app.UseEventBus<AddIntegrationEvent, AddIntegrationEventHandler>();

app.Run();
