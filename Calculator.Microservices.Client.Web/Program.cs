using Calculator.Microservices.Client.Web.IntegrationEvents.EventHandling;
using Calculator.Microservices.Client.Web.Services;
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
                .AddRabbitMQ(name: "rabbitmq_client_web", rabbitConnectionString: $"amqp://{configuration["RABBITMQ_HOSTNAME"]}")
                .AddKafka(
                    new ProducerConfig { BootstrapServers = configuration["KAFKA_BOOTSTRAP_SERVERS"] },
                    name: "kafka_client_web"
                 );

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<IMessageService, MessageService>();

builder.Services.AddEventBus(configuration);
builder.Services.AddTransient<ResultIntegrationEventHandler>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.Subscribe<ResultIntegrationEvent, ResultIntegrationEventHandler>();

app.UseStaticFiles();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
