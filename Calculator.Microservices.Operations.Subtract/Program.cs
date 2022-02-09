using Calculator.Microservices.Shared.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus("subtract");
builder.Services.AddTransient<SubtractIntegrationEventHandler>();

var app = builder.Build();

app.UseEventBus<SubtractIntegrationEvent, SubtractIntegrationEventHandler>();

app.Run();
