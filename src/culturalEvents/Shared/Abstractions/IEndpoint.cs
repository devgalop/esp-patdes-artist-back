using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace culturalEvents.Shared.Abstractions
{
    /// <summary>
    /// Define the interface for mapping endpoints in a web application. Classes that implement this interface will define how to map their specific endpoints using the provided IEndpointRouteBuilder.
    /// </summary>
    public interface IEndpoint
    {
        /// <summary>
        /// Defines the mapping of endpoints in the web application.
        /// </summary>
        /// <param name="app">The endpoint route builder used to configure the endpoints.</param>
        void MapEndpoint(IEndpointRouteBuilder app);
    }

    public static class EndpointExtensions
    {
        /// <summary>
        /// Registers the dependencies of all endpoints in the service container.
        /// </summary>
        /// <param name="builder">The web application builder used to configure services.</param>
        /// <returns>The web application builder with the endpoints added.</returns>
        public static WebApplicationBuilder AddEndpoints(this WebApplicationBuilder builder)
        {
            // Register all types that implement IEndpoint in the service container
               var serviceDescriptors = Assembly.GetExecutingAssembly()
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                               typeof(IEndpoint).IsAssignableFrom(type))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();

            builder.Services.TryAddEnumerable(serviceDescriptors);
            return builder;
        }

        /// <summary>
        /// Maps all registered endpoints in the web application.
        /// </summary>
        /// <param name="app">The web application.</param>
        /// <returns>The web application with the endpoints mapped.</returns>
        public static WebApplication MapEndpoints(this WebApplication app)
        {
            // Get all registered services that implement IEndpoint
            var endpoints = app.Services.GetServices<IEndpoint>();

            // Map each endpoint in the web application
            foreach (var endpoint in endpoints)
            {
                endpoint.MapEndpoint(app);
            }

            return app;
        }
    }
}