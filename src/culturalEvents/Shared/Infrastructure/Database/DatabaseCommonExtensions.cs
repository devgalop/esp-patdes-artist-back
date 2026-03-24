using Microsoft.EntityFrameworkCore;

namespace culturalEvents.Shared.Infrastructure.Database
{
    public static class DatabaseCommonExtensions
    {
        /// <summary>
        /// Agrega el contexto de la base de datos a los servicios de la aplicación, configurando la conexión a PostgreSQL y utilizando la convención de nomenclatura snake_case.
        /// </summary>
        /// <param name="builder">Constructor de la aplicación web</param>
        /// <returns>Constructor de la aplicación web con el contexto de la base de datos agregado</returns>
        public static WebApplicationBuilder AddDatabaseContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDatabaseContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Postgresql");
                options.UseNpgsql(connectionString)
                        .UseSnakeCaseNamingConvention(); 
            });

            return builder;
        }

        /// <summary>
        /// Verifica la conexión a la base de datos y registra el resultado en los logs.
        /// </summary>
        /// <param name="app">Aplicación web</param>
        /// <returns>Tarea asincrónica que representa la operación de verificación de la conexión a la base de datos</returns>
        public static async Task EnsureDatabaseCanConnectAsync(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();
            var canConnect = await db.Database.CanConnectAsync();
            app.Logger.LogInformation("Can connect to database: {CanConnect}", canConnect);
        }

        /// <summary>
        /// Aplica las migraciones pendientes a la base de datos.
        /// </summary>
        /// <param name="app">Aplicación web</param>
        /// <returns>Tarea asincrónica que representa la operación de aplicación de migraciones</returns>
        public static async Task ApplyMigrationsAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            await using AppDatabaseContext appContext =
                scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();

            try
            {
                await appContext.Database.MigrateAsync();
                app.Logger.LogInformation("Se aplicaron las migraciones de la base de datos correctamente.");
            }
            catch (Exception e)
            {
                app.Logger.LogError(e, "Ocurrió un error al aplicar las migraciones de la base de datos.");
            }
        }

        /// <summary>
        /// Reinicia la base de datos eliminándola y aplicando las migraciones.
        /// Solo para uso en desarrollo o pruebas.
        /// </summary>
        /// <param name="app">Aplicación web</param>
        /// <returns>Tarea asincrónica que representa la operación de reinicio de la base de datos</returns>
        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using IServiceScope scope = app.Services.CreateScope();
            await using AppDatabaseContext appContext =
                scope.ServiceProvider.GetRequiredService<AppDatabaseContext>();

            try
            {
                await appContext.Database.EnsureDeletedAsync();
                await appContext.Database.MigrateAsync();
                app.Logger.LogInformation("Se reinició la base de datos correctamente.");
            }
            catch (Exception e)
            {
                app.Logger.LogError(e, "Ocurrió un error al reiniciar la base de datos.");
            }
        }
    }
}