using System;
using blog_domain.Primitives;

namespace blog_domain.Entities;

public sealed class User
{
    public UserId Id { get; }

    public Username Username { get; }

    public EmailAddress Email { get; private set; }

    private User(UserId id, Username username, EmailAddress email)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }

    public static User Create(UserId id, Username username, EmailAddress email)
        => new(id, username, email);

    public void ChangeEmail(EmailAddress email)
    {
        if (email is null)
        {
            throw new ArgumentNullException(nameof(email));
        }

        Email = email;
    }
}
