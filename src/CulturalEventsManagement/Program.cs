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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseDatabaseMigrations(); // 👈 PRIMERO (CLAVE)

app.UseCors("AllowAll");

app.MapEndpoints();

app.MapOpenApi();

app.MapScalarApiReference("/docs", options =>
{
    options.Title = "API Gestión de Eventos Culturales";
    options.WithOpenApiRoutePattern("/openapi/v1.json");
    options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    options.DarkMode = true;
});

app.MapGet("/", () => Results.Redirect("/docs"));

app.UseHttpsRedirection();

await app.RunAsync();

