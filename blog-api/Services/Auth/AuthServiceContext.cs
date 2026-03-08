using blog_application.Ports;
using blog_application.Services;

namespace blog_api.Services.Auth;

public sealed class AuthServiceContext
{
    public AuthServiceContext(
        IUserRepository userRepository,
        ICredentialRepository credentialRepository,
        IPasswordHasher passwordHasher,
        IMessageBus messageBus)
    {
        UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        CredentialRepository = credentialRepository ?? throw new ArgumentNullException(nameof(credentialRepository));
        PasswordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        MessageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
    }

    public IUserRepository UserRepository { get; }

    public ICredentialRepository CredentialRepository { get; }

    public IPasswordHasher PasswordHasher { get; }

    public IMessageBus MessageBus { get; }
}
