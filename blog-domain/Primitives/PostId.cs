using System;

namespace blog_domain.Primitives;

public sealed class PostId
{
    public Guid Value { get; }

    private PostId(Guid value) => Value = value;

    public static PostId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidPostIdException();
        }

        return new PostId(value);
    }
}

public sealed class InvalidPostIdException : Exception
{
    public InvalidPostIdException()
        : base("Post identifiers cannot be empty.")
    {
    }
}
