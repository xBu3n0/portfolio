using System;
using System.Threading;
using System.Threading.Tasks;
using blog_application.Commands.LoginUser;
using blog_application.Ports;
using blog_application.Services;
using blog_domain.Entities;
using blog_domain.Primitives;
using Moq;
using Xunit;

namespace blog_tests.Application.Commands.LoginUser;

public class LoginUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_validates_password_before_returning_user()
    {
        // Arrange
        var userRepository = new Mock<IUserRepository>();
        var credentialRepository = new Mock<ICredentialRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var handler = new LoginUserCommandHandler(userRepository.Object, credentialRepository.Object, passwordHasher.Object);

        var expectedUser = User.Create(
            UserId.Create(Guid.NewGuid()),
            Username.Create("login-user"),
            EmailAddress.Create("found@example.com")
        );

        var command = new LoginUserCommand(
            expectedUser.Username,
            Password.Create("Secur3P@ss")
        );

        credentialRepository.Setup(repo => repo.GetPasswordHashAsync(command.Username, It.IsAny<CancellationToken>()))
            .ReturnsAsync("hashed");
        passwordHasher.Setup(hasher => hasher.Verify(command.Password, "hashed")).Returns(true);
        userRepository.Setup(repo => repo.GetByUsernameAsync(command.Username, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedUser);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Same(expectedUser, result);
        credentialRepository.Verify(repo => repo.GetPasswordHashAsync(command.Username, It.IsAny<CancellationToken>()), Times.Once);
        passwordHasher.Verify(hasher => hasher.Verify(command.Password, "hashed"), Times.Once);
        userRepository.Verify(repo => repo.GetByUsernameAsync(command.Username, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_returns_null_when_password_is_invalid()
    {
        // Arrange
        var userRepository = new Mock<IUserRepository>();
        var credentialRepository = new Mock<ICredentialRepository>();
        var passwordHasher = new Mock<IPasswordHasher>();
        var handler = new LoginUserCommandHandler(userRepository.Object, credentialRepository.Object, passwordHasher.Object);

        var command = new LoginUserCommand(
            Username.Create("login-user"),
            Password.Create("WrongPass123")
        );

        credentialRepository.Setup(repo => repo.GetPasswordHashAsync(command.Username, It.IsAny<CancellationToken>()))
            .ReturnsAsync("hashed");
        passwordHasher.Setup(hasher => hasher.Verify(command.Password, "hashed")).Returns(false);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Null(result);
        credentialRepository.Verify(repo => repo.GetPasswordHashAsync(command.Username, It.IsAny<CancellationToken>()), Times.Once);
        passwordHasher.Verify(hasher => hasher.Verify(command.Password, "hashed"), Times.Once);
        userRepository.Verify(repo => repo.GetByUsernameAsync(It.IsAny<Username>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}
