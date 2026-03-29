using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using culturalEvents.Modules.UserManagement.Common;
using culturalEvents.Modules.UserManagement.CreateUser;
using culturalEvents.Shared.Abstractions;
using culturalEvents.Shared.Domain;
using Moq;
using Xunit;

namespace CulturalEventsTests.Modules.UserManagement
{
    public class CreateUserHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<IRoleRepository> _roleRepository;
        private readonly Mock<ICreedentialsManager<UserCreedentials>> _creedentialsManager;

        private readonly CreateUserHandler _handler;

        public CreateUserHandlerTests()
        {
            _userRepository = CreateUserRepository();
            _creedentialsManager = CreateCreedentialsManager();
            _roleRepository = CreateRoleRepository();
            _handler = new CreateUserHandler(_creedentialsManager.Object, _userRepository.Object, _roleRepository.Object);
        }

        private static CreateUserRequest CreateValidRequest()
        {
            return new CreateUserRequest(
                "John",
                "john.doe@example.com",
                "Password123!"
            );
        }

        private static Mock<IUserRepository> CreateUserRepository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync((User)null!);
            return userRepositoryMock;
        }

        private static Mock<IRoleRepository> CreateRoleRepository()
        {
            const string DEFAULT_ROLE = "Consumer";
            var roleRepositoryMock = new Mock<IRoleRepository>();
            roleRepositoryMock.Setup(r => r.GetRoleByName(It.IsAny<string>()))
                .ReturnsAsync(new Role(DEFAULT_ROLE, "Default role for new users"));
            return roleRepositoryMock;
        }

        private static Mock<ICreedentialsManager<UserCreedentials>> CreateCreedentialsManager()
        {
            var creedentialsManagerMock = new Mock<ICreedentialsManager<UserCreedentials>>();
            creedentialsManagerMock.Setup(m => m.HashPassword(It.IsAny<UserCreedentials>(), It.IsAny<string>()))
                .Returns("hashedPassword");
            return creedentialsManagerMock;
        }

        [Fact]
        public async Task CreateUser_WhenDoesntExist_ShouldCreateUser()
        {
            var request = CreateValidRequest();

            await _handler.HandleAsync(request);

            _userRepository.Verify(r => r.AddUser(It.IsAny<User>()), Times.Once);
            _creedentialsManager.Verify(m => m.HashPassword(It.IsAny<UserCreedentials>(), It.IsAny<string>()), Times.Once);
            _roleRepository.Verify(r => r.GetRoleByName(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateUser_WhenUserAlreadyExists_ShouldThrowException()
        {
            var request = CreateValidRequest();
            _userRepository.Setup(r => r.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(new User(request.Name, request.Email, "hashedPassword"));

            await Assert.ThrowsAsync<AlreadyExistingUserException>(() => _handler.HandleAsync(request));

            _userRepository.Verify(r => r.AddUser(It.IsAny<User>()), Times.Never);
            _creedentialsManager.Verify(m => m.HashPassword(It.IsAny<UserCreedentials>(), It.IsAny<string>()), Times.Never);
            _roleRepository.Verify(r => r.GetRoleByName(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task CreateUser_WhenDefaultRoleNotFound_ShouldThrowException()
        {
            var request = CreateValidRequest();
            _roleRepository.Setup(r => r.GetRoleByName(It.IsAny<string>()))
                .ReturnsAsync((Role)null!);

            await Assert.ThrowsAsync<RoleNotFoundException>(() => _handler.HandleAsync(request));

            _userRepository.Verify(r => r.AddUser(It.IsAny<User>()), Times.Never);
            _creedentialsManager.Verify(m => m.HashPassword(It.IsAny<UserCreedentials>(), It.IsAny<string>()), Times.Never);
            _roleRepository.Verify(r => r.GetRoleByName(It.IsAny<string>()), Times.Once);
        }
    }
}