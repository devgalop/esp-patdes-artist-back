using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using culturalEvents.Modules.UserManagement.Common;
using culturalEvents.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace culturalEvents.Shared.Infrastructure.Database.Repositories
{
    public sealed class UserRepository(AppDatabaseContext dbContext) : IUserRepository
    {
        public async Task AddUser(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await dbContext.Users
            .Include(u => u.Roles).ToListAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await dbContext.Users.Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Email.Equals(email));
        }

        public async Task<User?> GetUserById(string id)
        {
            return await dbContext.Users.Include(u => u.Roles)
            .FirstOrDefaultAsync(u => u.Id.ToString().Equals(id));
        }

        public async Task UpdateUser(User user)
        {
            dbContext.Users.Update(user);
            await dbContext.SaveChangesAsync();
        }
    }
}