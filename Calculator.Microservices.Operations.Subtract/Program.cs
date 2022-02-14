using Calculator.Microservices.Shared.Extensions;
using Calculator.Microservices.Operations.Subtract.IntegrationEvents.EventHandling;
using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddRabbitMQ(name: "rabbitmq_subtract_service", rabbitConnectionString: $"amqp://{configuration["RABBITMQ_HOSTNAME"]}")
                .AddKafka(
                    new ProducerConfig { BootstrapServers = configuration["KAFKA_BOOTSTRAP_SERVERS"] },
                    name: "kafka_subtract_service"
                 );

builder.Services.AddEventBus(configuration);
builder.Services.AddTransient<SubtractIntegrationEventHandler>();

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

app.Subscribe<SubtractIntegrationEvent, SubtractIntegrationEventHandler>();

app.Run();
