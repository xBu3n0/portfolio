using System;

namespace blog_domain.Primitives;

public sealed class CommentId
{
    public Guid Value { get; }

    private CommentId(Guid value) => Value = value;

    public static CommentId Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidCommentIdException();
        }

        return new CommentId(value);
    }
}

public sealed class InvalidCommentIdException : Exception
{
    public InvalidCommentIdException()
        : base("Comment identifiers cannot be empty.")
    {
    }
}
