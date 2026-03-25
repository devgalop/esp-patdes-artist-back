using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace culturalEvents.Shared.Abstractions
{
    /// <summary>
    /// Interface for managing user credentials, including password hashing and verification.
    /// </summary>
    /// <typeparam name="TUser">Type of the user entity</typeparam>
    public interface ICreedentialsManager<in TUser>
    {
        /// <summary>
        /// Hashed password using a secure hashing algorithm (e.g., bcrypt, PBKDF2, Argon2)
        /// </summary>
        /// <param name="user">User entity</param>
        /// <param name="password">Plain text password</param>
        /// <returns>Hashed password</returns>
        string HashPassword(TUser user, string password);
        /// <summary>
        /// Verifies if the provided password matches the hashed password
        /// </summary>
        /// <param name="user">User entity</param>
        /// <param name="hashedPassword">Hashed password</param>
        /// <param name="providedPassword">Plain text password</param>
        /// <returns>Validation result</returns>
        bool VerifyHashedPassword(TUser user, string hashedPassword, string providedPassword);
    }
}