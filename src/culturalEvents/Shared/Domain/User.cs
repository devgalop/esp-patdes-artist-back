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

        public User()
        {
            Id = Guid.CreateVersion7();
            Name = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
        }

        public User(string name, string email, string passwordHash)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
        }
    }
}