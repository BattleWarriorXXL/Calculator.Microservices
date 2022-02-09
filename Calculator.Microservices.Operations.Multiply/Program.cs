using Calculator.Microservices.Shared.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus("multiply");
builder.Services.AddTransient<MultiplyIntegrationEventHandler>();

var app = builder.Build();

app.UseEventBus<MultiplyIntegrationEvent, MultiplyIntegrationEventHandler>();

app.Run();
