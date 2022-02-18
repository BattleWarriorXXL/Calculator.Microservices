using Calculator.Microservices.Client.Web.IntegrationEvents.EventHandling;
using Calculator.Microservices.Client.Web.Services;
using Calculator.Microservices.Shared.Extensions;
using Calculator.Microservices.Shared.IntegrationEvents.Events;
using Calculator.Microservices.Shared.Library.HealthCheck;
using Calculator.Microservices.Shared.Library.HealthCheck.KafkaHealthCheck;
using Calculator.Microservices.Shared.Library.HealthCheck.RabbitMQHealthCheck;
using Calculator.Microservices.Shared.Library.HealthCheck.SelfHealthCheck;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddHealthChecks()
                .AddSelfCheck()
                .AddRabbitMQCheck(configuration["RABBITMQ_HOSTNAME"])
                .AddKafkaCheck(configuration["KAFKA_BOOTSTRAP_SERVERS"]);

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
        ResponseWriter = HealthResponseWriter.WriteHealthCheckResponse
    });
    endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
    {
        Predicate = r => r.Name.Contains("self")
    });
});

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
