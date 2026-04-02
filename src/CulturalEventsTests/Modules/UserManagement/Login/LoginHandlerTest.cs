using culturalEvents.Modules.UserManagement.Common;
using culturalEvents.Modules.UserManagement.Login;
using culturalEvents.Shared.Abstractions;
using culturalEvents.Shared.Domain;
using FluentValidation;
using Moq;
using Xunit;

namespace CulturalEventsTests.Modules.UserManagement.Login;

public class LoginHandlerTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<ICreedentialsManager<UserCreedentials>> _creedentialsManager;
    private readonly LoginHandler _handler;

    public LoginHandlerTest()
    {
        _userRepository = CreateUserRepository();
        _creedentialsManager = CreateCreedentialsManager();
        _handler = new LoginHandler(_creedentialsManager.Object, _userRepository.Object);
    }

    private static LoginRequest CreateValidRequest()
    {
        return new LoginRequest(
            "test@example.com",
            "Password123!*"
        );
    }

    private static Mock<IUserRepository> CreateUserRepository()
    {
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(r => r.GetUserByEmail(It.IsAny<string>()))
            .ReturnsAsync((User)null!);
        return userRepositoryMock;
    }

    private static Mock<ICreedentialsManager<UserCreedentials>> CreateCreedentialsManager()
    {
        var creedentialsManagerMock = new Mock<ICreedentialsManager<UserCreedentials>>();
        creedentialsManagerMock.Setup(m => m.HashPassword(It.IsAny<UserCreedentials>(), It.IsAny<string>()))
            .Returns("hashedPassword");
        creedentialsManagerMock.Setup(m => m.VerifyHashedPassword(It.IsAny<UserCreedentials>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);
        return creedentialsManagerMock;
    }

    [Fact]
    public async Task Login_WhenUserExistsAndPasswordIsCorrect_ShouldReturnSuccess()
    {
        LoginRequest request = CreateValidRequest();
        _userRepository.Setup(r => r.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(new User("name", request.Email, "hashedPassword"));
        await _handler.HandleAsync(request);
        _userRepository.Verify(r => r.GetUserByEmail(request.Email), Times.Once);
        _creedentialsManager.Verify(m => m.VerifyHashedPassword(It.IsAny<UserCreedentials>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Login_WhenUserDoesNotExist_ShouldReturnFailure()
    {
        LoginRequest request = CreateValidRequest();
        await Assert.ThrowsAsync<ValidationException>(() =>_handler.HandleAsync(request));
        _userRepository.Verify(r => r.GetUserByEmail(request.Email), Times.Once);
        _creedentialsManager.Verify(m => m.VerifyHashedPassword(It.IsAny<UserCreedentials>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    [Fact]
    public async Task Login_WhenPasswordIsIncorrect_ShouldReturnFailure()
    {
        LoginRequest request = CreateValidRequest();
        _userRepository.Setup(r => r.GetUserByEmail(It.IsAny<string>()))
                .ReturnsAsync(new User("name", request.Email, "hashedPassword"));
        _creedentialsManager.Setup(m => m.VerifyHashedPassword(It.IsAny<UserCreedentials>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);
        await Assert.ThrowsAsync<ValidationException>(() => _handler.HandleAsync(request));
        _userRepository.Verify(r => r.GetUserByEmail(request.Email), Times.Once);
        _creedentialsManager.Verify(m => m.VerifyHashedPassword(It.IsAny<UserCreedentials>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
    }
}
