using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using culturalEvents.Shared.Abstractions;

namespace culturalEvents.Modules.UserManagement.CreateUser
{
    /// <summary>
    /// Extension methods for registering the CreateUser feature in the dependency injection container.
    /// </summary>
    public static class CreateUserExtensions
    {
        /// <summary>
        /// Registers the CreateUser feature in the dependency injection container.
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder instance</param>
        /// <returns>The WebApplicationBuilder instance</returns>
        public static WebApplicationBuilder RegisterCreateUserFeature(this WebApplicationBuilder builder)
        {            
            builder.Services.AddScoped<ICommandHandler<CreateUserRequest>, CreateUserHandler>();
            return builder;
        }
    }
}