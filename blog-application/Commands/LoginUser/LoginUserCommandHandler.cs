using System.Threading;
using System.Threading.Tasks;
using blog_application.Ports;
using blog_application.Services;
using blog_domain.Entities;

namespace blog_application.Commands.LoginUser;

public sealed class LoginUserCommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ICredentialRepository _credentialRepository;
    private readonly IPasswordHasher _passwordHasher;

    public LoginUserCommandHandler(
        IUserRepository userRepository,
        ICredentialRepository credentialRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _credentialRepository = credentialRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> Handle(LoginUserCommand command, CancellationToken cancellationToken = default)
    {
        var passwordHash = await _credentialRepository.GetPasswordHashAsync(command.Username, cancellationToken);
        if (passwordHash is null)
        {
            return null;
        }

        if (!_passwordHasher.Verify(command.Password, passwordHash))
        {
            return null;
        }

        return await _userRepository.GetByUsernameAsync(command.Username, cancellationToken);
    }
}
