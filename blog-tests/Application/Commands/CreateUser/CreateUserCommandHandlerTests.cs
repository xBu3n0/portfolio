using System;
using System.Threading;
using System.Threading.Tasks;
using blog_application.Commands.CreateUser;
using blog_application.Ports;
using blog_domain.Entities;
using blog_domain.Events;
using blog_domain.Primitives;
using Moq;
using Xunit;

namespace blog_tests.Application.Commands.CreateUser;

public class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_persists_user_and_dispatches_event()
    {
        // Arrange
        var userRepository = new Mock<IUserRepository>();
        var messageBus = new Mock<IMessageBus>();
        var handler = new CreateUserCommandHandler(userRepository.Object, messageBus.Object);

        var newUser = User.Create(
            UserId.Create(Guid.NewGuid()),
            Username.Create("new-user"),
            EmailAddress.Create("some@email.com")
        );

        userRepository.Setup(repo => repo.AddAsync(
            It.IsAny<User>(),
            It.IsAny<CancellationToken>()))
        .ReturnsAsync(newUser);

        IDomainEvent[] dispatchedEvents = Array.Empty<IDomainEvent>();
        messageBus.Setup(bus => bus.PublishAsync(
            It.IsAny<IDomainEvent[]>(),
            It.IsAny<CancellationToken>()))
        .Callback<IDomainEvent[], CancellationToken>((events, _) => dispatchedEvents = events)
        .Returns(Task.CompletedTask);

        var command = new CreateUserCommand(
            newUser.Id,
            newUser.Username,
            newUser.Email
        );

        // Act
        var createdUser = await handler.Handle(command, CancellationToken.None);

        // Assert
        userRepository.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
        messageBus.Verify(bus => bus.PublishAsync(It.IsAny<IDomainEvent[]>(), It.IsAny<CancellationToken>()), Times.Once);

        Assert.Contains(dispatchedEvents, e => e is UserCreatedEvent);
        
        Assert.NotNull(createdUser);
        Assert.Equal(command.UserId, createdUser.Id);
        Assert.Equal(command.Username, createdUser.Username);
        Assert.Equal(command.Email, createdUser.Email);
    }
}
