using Microsoft.AspNetCore.Diagnostics;

namespace CulturalEventsManagement.Middlewares;

public sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService
) : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, 
                                            Exception exception, 
                                            CancellationToken cancellationToken)
    {
        return problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new()
            {
                Title = "Error interno del servidor",
                Detail = $"Ha ocurrido un error inesperado mientras se procesaba la solicitud. Por favor, intente nuevamente más tarde. {exception.Message}",
                Status = StatusCodes.Status500InternalServerError
            }
        });
    }
}