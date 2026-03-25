using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using culturalEvents.Modules.UserManagement.CreateUser;

namespace culturalEvents.Modules.UserManagement.Common
{
    /// <summary>
    /// The UserCommonExtensions class provides extension methods for registering user management features in the application.
    /// </summary>
    public static class UserCommonExtensions
    {
        /// <summary>
        /// Registers all user management features
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder instance</param>
        /// <returns>The WebApplicationBuilder instance</returns>
        public static WebApplicationBuilder RegisterUserManagementFeatures(this WebApplicationBuilder builder)
        {
            builder.RegisterCreateUserFeature()
                   .RegisterUserCreedentialsManager();
            return builder;
        }
    }
}