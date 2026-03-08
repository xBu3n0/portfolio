using blog_domain.Primitives;

namespace blog_application.Services.Auth.Commands.LoginUser;

public sealed record LoginUserCommand(Username Username, Password Password);
