using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using culturalEvents.Shared.Domain;

namespace culturalEvents.Modules.UserManagement.Common
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Adds a new role to the repository.
        /// </summary>
        /// <param name="role">The role to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddRole(Role role);
        /// <summary>
        /// Updates an existing role in the repository.
        /// </summary>
        /// <param name="role">The role to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateRole(Role role);
        /// <summary>
        /// Gets a role by its ID.
        /// </summary>
        /// <param name="id">The ID of the role.</param>
        /// <returns>The task result contains the role if found; otherwise, null.</returns>
        Task<Role?> GetRoleById(string id);
        /// <summary>
        /// Gets a role by its name.
        /// </summary>
        /// <param name="name">The name of the role.</param>
        /// <returns>The task result contains the role if found; otherwise, null.</returns>
        Task<Role?> GetRoleByName(string name);
        /// <summary>
        /// Gets all roles in the repository.
        /// </summary>
        /// <returns>The task result contains a collection of all roles.</returns>
        Task<IEnumerable<Role>> GetAllRoles();
    }
}