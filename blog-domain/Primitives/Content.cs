using System;

namespace blog_domain.Primitives;

public sealed class Content
{
    public const int MaxLength = 10000;

    public string Value { get; }

    private Content(string value) => Value = value;

    public static Content Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length > MaxLength)
        {
            throw new InvalidContentException();
        }

        return new Content(value);
    }
}

public sealed class InvalidContentException : Exception
{
    public InvalidContentException()
        : base($"Content must be between 1 and {Content.MaxLength} characters.")
    {
    }
}
