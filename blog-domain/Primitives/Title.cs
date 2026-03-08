using System;

namespace blog_domain.Primitives;

public sealed class Title
{
    public const int MaxLength = 500;

    public string Value { get; }

    private Title(string value) => Value = value;

    public static Title Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MaxLength)
        {
            throw new InvalidTitleException();
        }

        return new Title(value);
    }
}

public sealed class InvalidTitleException : Exception
{
    public InvalidTitleException()
        : base($"Title must be between 1 and {Title.MaxLength} characters.")
    {
    }
}
