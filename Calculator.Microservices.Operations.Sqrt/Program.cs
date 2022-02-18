using Calculator.Microservices.Shared.Extensions;
using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Calculator.Microservices.Operations.Sqrt.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.Library.HealthCheck;
using Calculator.Microservices.Shared.Library.HealthCheck.SelfHealthCheck;
using Calculator.Microservices.Shared.Library.HealthCheck.RabbitMQHealthCheck;
using Calculator.Microservices.Shared.Library.HealthCheck.KafkaHealthCheck;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddHealthChecks()
                .AddSelfCheck()
                .AddRabbitMQCheck(configuration["RABBITMQ_HOSTNAME"])
                .AddKafkaCheck(configuration["KAFKA_BOOTSTRAP_SERVERS"]);

builder.Services.AddEventBus(configuration);
builder.Services.AddTransient<SqrtIntegrationEventHandler>();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = HealthResponseWriter.WriteHealthCheckResponse
    });
    endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
    {
        Predicate = r => r.Name.Contains("self")
    });
});

app.Subscribe<SqrtIntegrationEvent, SqrtIntegrationEventHandler>();

app.Run();
