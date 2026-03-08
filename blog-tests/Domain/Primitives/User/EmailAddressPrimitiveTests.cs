using System;
using blog_domain.Primitives;
using Xunit;

namespace blog_tests.Domain.Primitives.User;

public class EmailAddressPrimitiveTests
{
    [Fact]
    public void Rejects_invalid_format()
    {
        // Arrange
        var invalidEmail = "missing-at-sign.com";

        // Act
        Action act = () => EmailAddress.Create(invalidEmail);

        // Assert
        Assert.Throws<InvalidEmailException>(act);
    }

    [Fact]
    public void Rejects_empty_or_whitespace_input()
    {
        // Arrange
        var whitespaceEmail = "   ";

        // Act
        Action act = () => EmailAddress.Create(whitespaceEmail);

        // Assert
        Assert.Throws<InvalidEmailException>(act);
    }
}
