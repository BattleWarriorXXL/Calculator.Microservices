using Calculator.Microservices.Operations.Pow.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.Extensions;
using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Confluent.Kafka;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddRabbitMQ(name: "rabbitmq_pow_service", rabbitConnectionString: $"amqp://{configuration["RABBITMQ_HOSTNAME"]}")
                .AddKafka(
                    new ProducerConfig { BootstrapServers = configuration["KAFKA_BOOTSTRAP_SERVERS"] },
                    name: "kafka_pow_service"
                 );

builder.Services.AddEventBus(configuration);
builder.Services.AddTransient<PowIntegrationEventHandler>();

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

app.Subscribe<PowIntegrationEvent, PowIntegrationEventHandler>();

app.Run();
