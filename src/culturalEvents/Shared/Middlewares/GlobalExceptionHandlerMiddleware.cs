using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;

namespace culturalEvents.Shared.Middlewares
{
    public sealed class GlobalExceptionHandlerMiddleware(
        IProblemDetailsService problemDetailsService
    ) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new()
                {
                    Title = "Internal Server Error",
                    Detail = $"An unexpected error occurred while processing the request. Please try again later. {exception.Message}",
                    Status = StatusCodes.Status500InternalServerError
                }
            });
        }
    }
}