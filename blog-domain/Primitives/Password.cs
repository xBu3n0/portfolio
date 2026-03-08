using System;

namespace blog_domain.Primitives;

public sealed class Password
{
    private const int MinLength = 8;

    public string Value { get; }

    private Password(string value) => Value = value;

    public static Password Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || value.Length < MinLength || !HasRequiredComplexity(value))
        {
            throw new WeakPasswordException();
        }

        return new Password(value);
    }

    private static bool HasRequiredComplexity(string value)
    {
        var hasUpper = false;
        var hasLower = false;
        var hasDigit = false;

        foreach (var c in value)
        {
            if (char.IsUpper(c)) hasUpper = true;
            if (char.IsLower(c)) hasLower = true;
            if (char.IsDigit(c)) hasDigit = true;
        }

        return hasUpper && hasLower && hasDigit;
    }
}

public sealed class WeakPasswordException : Exception
{
    public WeakPasswordException()
        : base("Password must be at least 8 characters and include uppercase, lowercase, and digits.")
    {
    }
}
