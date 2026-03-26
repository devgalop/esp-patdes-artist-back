using culturalEvents.Modules.UserManagement.Common;
using culturalEvents.Shared.Abstractions;
using culturalEvents.Shared.Infrastructure.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.AddDatabaseContext()
       .AddEndpoints()
       .RegisterUserManagementFeatures();

var app = builder.Build();

await app.EnsureDatabaseCanConnectAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    await app.ResetDatabaseAsync();
    await app.SeedDatabaseAsync();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.MapEndpoints();

await app.RunAsync();
