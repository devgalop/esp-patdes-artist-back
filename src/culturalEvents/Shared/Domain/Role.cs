namespace culturalEvents.Shared.Domain
{
    public enum RoleStatus
    {
        ACTIVE = 1,
        INACTIVE = 0
    }
    
    public sealed class Role
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RoleStatus Status { get; private set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Permission> Permissions { get; set; }

        public Role()
        {
            Id = Guid.CreateVersion7();
            Name = string.Empty;
            Description = string.Empty;
            Status = RoleStatus.ACTIVE; 
            Permissions = new List<Permission>();
            Users = new List<User>();
        }

        public Role(string name, string description)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Description = description;
            Status = RoleStatus.ACTIVE;
            Permissions = new List<Permission>();
            Users = new List<User>();
        }

        public void UpdateStatus(RoleStatus status)
        {
            Status = status;
        }

        public void AddPermission(Permission permission)
        {
            Permissions.Add(permission);
        }
    }
}