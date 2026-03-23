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
        public PermissionStatus Status { get; private set; }

        public Permission()
        {
            Id = Guid.CreateVersion7();
            Name = string.Empty;
            Status = PermissionStatus.ACTIVE;
        }

        public Permission(string name)
        {
            Id = Guid.CreateVersion7();
            Name = name;
            Status = PermissionStatus.ACTIVE;
        }

        public void UpdateStatus(PermissionStatus status)
        {
            Status = status;
        }
    }
}