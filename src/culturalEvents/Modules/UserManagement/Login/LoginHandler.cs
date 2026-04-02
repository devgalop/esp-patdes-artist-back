using culturalEvents.Modules.UserManagement.Common;
using culturalEvents.Shared.Abstractions;
using FluentValidation;
using FluentValidation.Results;

namespace culturalEvents.Modules.UserManagement.Login;

public sealed class LoginHandler(
    ICreedentialsManager<UserCreedentials> credentialsManager,
    IUserRepository userRepository
) : ICommandHandler<LoginRequest>
{
    public async Task HandleAsync(LoginRequest command)
    {
        var userFound = await userRepository.GetUserByEmail(command.Email) 
                            ?? throw new ValidationException([
                                new ValidationFailure(
                                    nameof(command.Email), 
                                    $"User with email '{command.Email}' was not found."
                                )
                            ]);
        UserCreedentials credentials = new UserCreedentials(command.Email, command.Password);
        if (!credentialsManager.VerifyHashedPassword(credentials, userFound.PasswordHash, command.Password))
            throw new ValidationException([
                new ValidationFailure(
                    nameof(command.Password), 
                    "The provided password is incorrect."
                )
            ]);
    }
}
