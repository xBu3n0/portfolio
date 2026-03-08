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

    public Task<User?> Handle(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        return _userRepository.AddAsync(User.Create(command.UserId, command.Username, command.Email), cancellationToken)
            .ContinueWith(userTask =>
            {
                var user = userTask.Result;
                if (user is not null)
                {
                    _messageBus.PublishAsync(
                        [
                            new UserCreatedEvent(user.Id, user.Username, user.Email)
                        ],
                            cancellationToken
                        );
                }
                return user;
            }, cancellationToken);
    }
}
