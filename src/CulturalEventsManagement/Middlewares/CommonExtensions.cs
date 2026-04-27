namespace CulturalEventsManagement.Middlewares;

public static class CommonExtensions
{
    /// <summary>
    /// Agrega los manejadores de excepciones personalizados al pipeline de la aplicación.
    /// </summary>
    /// <param name="builder">El constructor de la aplicación web.</param>
    /// <returns>El mismo constructor de la aplicación web, para permitir encadenamiento de llamadas.</returns>
    public static WebApplicationBuilder AddExceptionHandlers(this WebApplicationBuilder builder)
    {
        builder.Services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
            };
        });
        builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        return builder;
    }
}
