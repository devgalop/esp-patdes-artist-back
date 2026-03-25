using culturalEvents.Shared.Abstractions;

namespace culturalEvents.Modules.UserManagement.CreateUser
{
    public record CreateUserRequest
    (
        string Name,
        string Email,
        string Password
    ): ICommand;
}
