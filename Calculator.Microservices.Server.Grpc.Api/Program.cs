using Calculator.Microservices.Server.Grpc.Api;

var builder = WebApplication.CreateBuilder(args);

var allowedCorsOrigin = builder.Configuration.GetSection("ALLOWED_CORS_ORIGIN").Get<string>();

Console.WriteLine(allowedCorsOrigin);

builder.Services.AddGrpc();

builder.Services.AddCors(setup =>
{
    setup.AddPolicy("Policy", builder => builder.WithOrigins(allowedCorsOrigin)
                                                .AllowAnyMethod()
                                                .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("Policy");

app.UseGrpcWeb();
app.MapGrpcService<CalculatorService>().EnableGrpcWeb();

app.Run();
