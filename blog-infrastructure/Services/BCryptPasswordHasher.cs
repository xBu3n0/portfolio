using System;
using blog_application.Services.Auth;
using blog_domain.Primitives;
using BCrypt.Net;

namespace blog_infrastructure.Services;

public sealed class BCryptPasswordHasher : IPasswordHasher
{
    public bool Verify(Password password, string storedHash)
    {
        if (password is null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        if (string.IsNullOrWhiteSpace(storedHash))
        {
            return false;
        }

        return BCrypt.Net.BCrypt.Verify(password.Value, storedHash);
    }

    public string Hash(Password password)
    {
        if (password is null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        return BCrypt.Net.BCrypt.HashPassword(password.Value);
    }
}
