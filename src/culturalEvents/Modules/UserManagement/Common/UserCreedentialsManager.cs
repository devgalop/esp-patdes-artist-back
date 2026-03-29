using culturalEvents.Shared.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace culturalEvents.Modules.UserManagement.Common
{
    /// <summary>
    /// Represents user credentials, including email and password. This record is used for authentication purposes, allowing users to log in to the system using their email and password. The UserCreedentialsManager class provides methods for hashing passwords and verifying hashed passwords, ensuring secure handling of user credentials.
    /// </summary>
    /// <param name="Email">The user's email address</param>
    /// <param name="Password">The user's password</param>
    public record UserCreedentials
    (
        string Email,
        string Password
    );

    /// <summary>
    /// Manages user credentials, including hashing passwords and verifying hashed passwords.
    /// </summary>
    /// <param name="passwordHasher">The password hasher instance</param>
    public sealed class UserCreedentialsManager(
        IPasswordHasher<UserCreedentials> passwordHasher
    ) : ICreedentialsManager<UserCreedentials>
    {
        public string HashPassword(UserCreedentials user, string password)
        {
            return passwordHasher.HashPassword(user, password);
        }

        public bool VerifyHashedPassword(UserCreedentials user, string hashedPassword, string providedPassword)
        {
            var result = passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }
    }

    /// <summary>
    /// Extension method for registering the UserCreedentialsManager in the dependency injection container.
    /// </summary>
    public static class UserCreedentialsManagerExtensions
    {
        /// <summary>
        /// Registers the UserCreedentialsManager and its dependencies in the dependency injection container.
        /// </summary>
        /// <param name="builder">The WebApplicationBuilder instance</param>
        /// <returns>The WebApplicationBuilder instance</returns>
        public static WebApplicationBuilder RegisterUserCreedentialsManager(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICreedentialsManager<UserCreedentials>, UserCreedentialsManager>()
                            .AddScoped<IPasswordHasher<UserCreedentials>, PasswordHasher<UserCreedentials>>();
            return builder;
        }
    }
}