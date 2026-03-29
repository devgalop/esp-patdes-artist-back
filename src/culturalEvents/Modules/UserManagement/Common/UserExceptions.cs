using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace culturalEvents.Modules.UserManagement.Common
{
    public class AlreadyExistingUserException(string email) 
    : Exception($"A user with the email '{email}' already exists.")
    {
    }

    public class RoleNotFoundException(string roleName)
    : Exception($"Role with name '{roleName}' was not found.")
    {
    }
}