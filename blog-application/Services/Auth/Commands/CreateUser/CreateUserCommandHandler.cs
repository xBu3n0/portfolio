using blog_application.Ports.Auth;
using blog_application.Ports.Core;
using blog_domain.Services.Auth.Entities;
using blog_domain.Services.Auth.Events;

namespace blog_application.Services.Auth.Commands.CreateUser;

public sealed class CreateUserCommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageBus _messageBus;

    public CreateUserCommandHandler(IUserRepository userRepository, IMessageBus messageBus)
    {
        _userRepository = userRepository;
        _messageBus = messageBus;
    }

    public async Task<User> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.AddAsync(User.Create(command.UserId, command.Username, command.Email), cancellationToken);

        if (user is null)
        {
            throw new InvalidOperationException("Failed to create user.");
        }

        await _messageBus.PublishAsync(
            [
                new UserCreatedEvent(user.Id, user.Username, user.Email)
            ],
            cancellationToken
        );

        return user;
    }
}
