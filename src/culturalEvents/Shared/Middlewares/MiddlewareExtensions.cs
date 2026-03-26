using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace culturalEvents.Shared.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static WebApplicationBuilder AddExceptionHandler(this WebApplicationBuilder builder)
        {
            builder.Services.AddProblemDetails(options =>
            {
                options.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Extensions["traceId"] = context.HttpContext.TraceIdentifier;
                };
            });
            builder.Services.AddExceptionHandler<ValidationExceptionHandlerMiddleware>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
            return builder;
        }
    }
}