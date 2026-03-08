using blog_api.Services.Auth.Contracts;
using blog_application.Commands.CreateUser;
using blog_application.Commands.LoginUser;
using blog_domain.Entities;
using blog_domain.Primitives;

namespace blog_api.Services.Auth;

public class AuthService
{
    private readonly AuthServiceContext _context;

    public AuthService(AuthServiceContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<User?> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var username = Username.Create(request.Username);
        var email = EmailAddress.Create(request.Email);

        var handler = new CreateUserCommandHandler(_context.UserRepository, _context.MessageBus);
        var command = new CreateUserCommand(
            UserId.Create(Guid.NewGuid()),
            username,
            email);

        return handler.Handle(command, cancellationToken);
    }

    public Task<User?> LoginUserAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var username = Username.Create(request.Username);
        var password = Password.Create(request.Password);

        var handler = new LoginUserCommandHandler(
            _context.UserRepository,
            _context.CredentialRepository,
            _context.PasswordHasher);
        var command = new LoginUserCommand(username, password);

        return handler.Handle(command, cancellationToken);
    }
}
