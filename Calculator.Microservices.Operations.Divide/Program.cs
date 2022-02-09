using Calculator.Microservices.Shared.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEventBus("divide");
builder.Services.AddTransient<DivideIntegrationEventHandler>();

var app = builder.Build();

app.UseEventBus<DivideIntegrationEvent, DivideIntegrationEventHandler>();

app.Run();
