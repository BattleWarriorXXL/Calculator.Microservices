const string ClientPath = "ClientApp";

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddSpaStaticFiles(options => options.RootPath = ClientPath);
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
    app.UseSpa(spaBuilder => spaBuilder.Options.SourcePath = ClientPath);
}

app.UseRouting();

app.Run();
