using blog_domain.Primitives;

namespace blog_application.Services;

public interface IPasswordHasher
{
    bool Verify(Password password, string storedHash);
}
