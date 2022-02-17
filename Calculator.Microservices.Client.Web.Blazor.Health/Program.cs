using Calculator.Microservices.Client.Web.Blazor.Health.Library.Configuration;
using Calculator.Microservices.Client.Web.Blazor.Health.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddHttpClient();

builder.Services.Configure<Settings>(configuration.GetSection("HealthCheck"));

builder.Services.AddHostedService<HealthCheckHostedService>();
builder.Services.AddSingleton<IHealthCheckReportService, HealthCheckReportService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
