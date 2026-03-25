using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using culturalEvents.Shared.Domain;

namespace culturalEvents.Modules.UserManagement.Common
{
    /// <summary>
    /// Interface for managing user data in the repository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        /// <param name="user">The user to add</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task AddUser(User user);
        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user</param>
        /// <returns>The user with the specified ID</returns>
        Task<User?> GetUserById(string id);
        /// <summary>
        /// Gets a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user</param>
        /// <returns>The user with the specified email address</returns>
        Task<User?> GetUserByEmail(string email);
        /// <summary>
        /// Gets all users in the repository.
        /// </summary>
        /// <returns>A collection of all users</returns>
        Task<IEnumerable<User>> GetAllUsers();
        /// <summary>
        /// Updates an existing user in the repository.
        /// </summary>
        /// <param name="user">The user to update</param>
        /// <returns>A task representing the asynchronous operation</returns>
        Task UpdateUser(User user);
    }
}