using blog_domain.Primitives;

namespace blog_application.Services.Auth;

public interface IPasswordHasher
{
    bool Verify(Password password, string storedHash);
}
