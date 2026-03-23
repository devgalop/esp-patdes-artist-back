using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace culturalEvents.Shared.Domain
{
    public sealed class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }

        public ICollection<Role> Roles { get; set; }

        public User()
        {
            Id = Guid.CreateVersion7();
            Name = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            Roles = new List<Role>();
        }

        public User(string name, string email, string passwordHash)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Roles = new List<Role>();
        }

        public void AddRole(Role role)
        {
            Roles.Add(role);
        }
    }
}