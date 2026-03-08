using System;

namespace blog_domain.Primitives;

public sealed class UserId
{
    public Guid Value { get; }

    private UserId(Guid value) => Value = value;

    public static UserId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidUserIdException();
        }

        return new UserId(value);
    }
}

public sealed class InvalidUserIdException : Exception
{
    public InvalidUserIdException()
        : base("User identifiers cannot be empty.")
    {
    }
}
