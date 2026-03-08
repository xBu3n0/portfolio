using blog_domain.Primitives;

namespace blog_application.Services.Auth.Commands.CreateUser;

public sealed record CreateUserCommand(UserId UserId, Username Username, EmailAddress Email);
