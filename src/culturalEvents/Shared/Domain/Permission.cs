namespace culturalEvents.Shared.Domain
{
    public enum PermissionStatus
    {
        ACTIVE = 1,
        INACTIVE = 0
    }

    public sealed class Permission
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public PermissionStatus Status { get; private set; }

        public ICollection<Role> Roles { get; set; }

        public Permission()
        {
            Id = Guid.CreateVersion7();
            Name = string.Empty;
            Description = string.Empty;
            Status = PermissionStatus.ACTIVE;
            Roles = new List<Role>();
        }

        public Permission(string name, string description)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Description = description;
            Status = PermissionStatus.ACTIVE;
            Roles = new List<Role>();
        }

        public void UpdateStatus(PermissionStatus status)
        {
            Status = status;
        }
    }
}