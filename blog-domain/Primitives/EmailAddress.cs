using System;

namespace blog_domain.Primitives;

public sealed class EmailAddress
{
    public string Value { get; }

    private EmailAddress(string value) => Value = value;

    public static EmailAddress Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value) || !HasValidStructure(value))
        {
            throw new InvalidEmailException();
        }

        return new EmailAddress(value);
    }

    private static bool HasValidStructure(string value)
    {
        var atIndex = value.IndexOf('@');
        if (atIndex <= 0)
        {
            return false;
        }

        var domainPart = value[(atIndex + 1)..];
        if (string.IsNullOrWhiteSpace(domainPart) || !domainPart.Contains('.'))
        {
            return false;
        }

        if (domainPart.EndsWith('.'))
        {
            return false;
        }

        return true;
    }
}

public sealed class InvalidEmailException : Exception
{
    public InvalidEmailException()
        : base("Email address is not valid.")
    {
    }
}
