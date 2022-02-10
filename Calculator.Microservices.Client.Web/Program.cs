using Calculator.Microservices.Client.Web.IntegrationEvents.EventHandling;
using Calculator.Microservices.Client.Web.Services;
using Calculator.Microservices.Shared.Extensions;
using Calculator.Microservices.Shared.IntegrationEvents.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<IMessageService, MessageService>();

builder.Services.AddEventBus("result");
builder.Services.AddTransient<ResultIntegrationEventHandler>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.Subscribe<ResultIntegrationEvent, ResultIntegrationEventHandler>();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
