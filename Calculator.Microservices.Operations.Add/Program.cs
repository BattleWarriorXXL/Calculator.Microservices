using Calculator.Microservices.Shared.Extensions;
using Calculator.Microservices.Operations.Add.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddEventBus("add");
builder.Services.AddTransient<AddIntegrationEventHandler>();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });
    endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
    {
        Predicate = r => r.Name.Contains("self")
    });
});

app.Subscribe<AddIntegrationEvent, AddIntegrationEventHandler>();

app.Run();
