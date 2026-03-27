using culturalEvents.Modules.UserManagement.Common;
using culturalEvents.Shared.Abstractions;
using culturalEvents.Shared.Infrastructure.Database;
using culturalEvents.Shared.Middlewares;
using FluentValidation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);
builder.Services.AddOpenApi();

builder.Services.AddOpenApi();

builder.AddDatabaseContext()
       .AddEndpoints()
       .AddMediator()
       .RegisterUserManagementFeatures()
       .AddExceptionHandler();

var app = builder.Build();

await app.EnsureDatabaseCanConnectAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs",options =>
    {
        options.Title = "Cultural Events Management API";
        options.WithOpenApiRoutePattern("/openapi/v1.json");
        options.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
        options.DarkMode = true;
    });
    await app.ResetDatabaseAsync();
    await app.SeedDatabaseAsync();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.MapEndpoints();

await app.RunAsync();
