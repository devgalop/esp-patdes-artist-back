using culturalEvents.Modules.UserManagement.Common;
using culturalEvents.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace culturalEvents.Shared.Infrastructure.Database.Repositories
{
    public sealed class RoleRepository(AppDatabaseContext dbContext) : IRoleRepository
    {
        public async Task AddRole(Role role)
        {
            dbContext.Roles.Add(role);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            dbContext.Roles.Include(r => r.Permissions);
            return await dbContext.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleById(string id)
        {
            dbContext.Roles.Include(r => r.Permissions);
            return await dbContext.Roles.FirstOrDefaultAsync(r => r.Id.ToString() == id);
        }

        public async Task<Role?> GetRoleByName(string name)
        {
            dbContext.Roles.Include(r => r.Permissions);
            return await dbContext.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task UpdateRole(Role role)
        {
            dbContext.Roles.Update(role);
            await dbContext.SaveChangesAsync();
        }
    }
}