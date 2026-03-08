using System;

namespace blog_domain.Primitives;

public sealed class Username
{
    public const int MinLength = 3;
    public const int MaxLength = 20;

    public string Value { get; }

    private Username(string value) => Value = value;

    public static Username Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < MinLength || value.Length > MaxLength)
        {
            throw new InvalidUsernameException();
        }

        return new Username(value);
    }
}

public sealed class InvalidUsernameException : Exception
{
    public InvalidUsernameException()
        : base($"Username must be between {Username.MinLength} and {Username.MaxLength} characters and cannot be whitespace.")
    {
    }
}
