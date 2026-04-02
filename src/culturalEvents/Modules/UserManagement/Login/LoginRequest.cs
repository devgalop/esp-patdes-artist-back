using culturalEvents.Shared.Abstractions;

namespace culturalEvents.Modules.UserManagement.Login;

public record LoginRequest(
    string Email,
    string Password
): ICommand;
