using culturalEvents.Modules.UserManagement.Common;
using culturalEvents.Shared.Abstractions;
using culturalEvents.Shared.Domain;

namespace culturalEvents.Modules.UserManagement.CreateUser
{
    public sealed class CreateUserHandler(
        ICreedentialsManager<UserCreedentials> credentialsManager,
        IUserRepository userRepository,
        IRoleRepository roleRepository
    ) : ICommandHandler<CreateUserRequest>
    {
        private const string DEFAULT_ROLE = "Consumer";

        /// <summary>
        /// Create a new user in the system.
        /// </summary>
        /// <param name="command">The request containing the user's details.</param>
        /// <returns>A task that represents the asynchronous operation of creating a user.</returns>
        /// <exception cref="AlreadyExistingUserException">Thrown when a user with the provided email already exists.</exception>
        /// <exception cref="RoleNotFoundException">Thrown when the default role (Consumer) is not found in the database.</exception>
        public async Task HandleAsync(CreateUserRequest command)
        {
            var userFound = await userRepository.GetUserByEmail(command.Email);
            if(userFound is not null)
            {
                throw new AlreadyExistingUserException(command.Email);
            }
            Role defaultRole = await roleRepository.GetRoleByName(DEFAULT_ROLE) ?? throw new RoleNotFoundException(DEFAULT_ROLE);
            UserCreedentials credentials = new UserCreedentials(command.Email, command.Password);
            string passwordHash = credentialsManager.HashPassword(credentials, command.Password);
            User entity = new(command.Name, command.Email, passwordHash);
            entity.AddRole(defaultRole);
            await userRepository.AddUser(entity);
        }
    }
}