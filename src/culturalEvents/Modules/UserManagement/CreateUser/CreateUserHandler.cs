using culturalEvents.Modules.UserManagement.Common;
using culturalEvents.Shared.Abstractions;
using culturalEvents.Shared.Domain;

namespace culturalEvents.Modules.UserManagement.CreateUser
{
    public sealed class CreateUserHandler(
        ICreedentialsManager<UserCreedentials> credentialsManager,
        IUserRepository userRepository
    ) : ICommandHandler<CreateUserRequest>
    {
        public async Task HandleAsync(CreateUserRequest command)
        {
            var userFound = await userRepository.GetUserByEmail(command.Email);
            if(userFound is not null)
            {
                throw new AlreadyExistingUserException(command.Email);
            }
            UserCreedentials credentials = new UserCreedentials(command.Email, command.Password);
            string passwordHash = credentialsManager.HashPassword(credentials, command.Password);
            User entity = new(command.Name, command.Email, passwordHash);
            await userRepository.AddUser(entity);
        }
    }
}