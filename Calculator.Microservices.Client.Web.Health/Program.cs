using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

builder.Services.AddOptions();
builder.Services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy());

builder.Services.AddHealthChecksUI(setupSettings: setup =>
                {
                    setup.SetEvaluationTimeInSeconds(5);
                    setup.MaximumHistoryEntriesPerEndpoint(50);
                })
                .AddInMemoryStorage();

var app = builder.Build();

app.UseHealthChecksUI(config =>
{
    config.ResourcesPath = "/ui/resources";
    config.UIPath = "/hc-ui";
});

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
    endpoints.MapHealthChecks("/liveness", new HealthCheckOptions
    {
        Predicate = r => r.Name.Contains("self")
    }); ;
});

app.Run();
