using CulturalEventsManagement.Infrastructure.Database.Shared;
using CulturalEventsManagement.Middlewares;
using CulturalEventsManagement.Modules.EventManagement.Shared;
using CulturalEventsManagement.Shared.Abstractions;
using FluentValidation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>();

builder.AddEndpoints()
        .AddMediator()
        .AddCulturalEventModule()
        .AddExceptionHandlers()
        .AddDatabaseDependencies();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs",options =>
    {
        options.Title = "API Gestión de Eventos Culturales";
        options.WithOpenApiRoutePattern("/openapi/v1.json");
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.DarkMode = true;
    });
    app.MapGet("/", () => Results.Redirect("/docs"));
    app.UseDatabaseMigrations();
}

app.UseHttpsRedirection();
app.EnsureDatabaseCreated();

await app.RunAsync();

