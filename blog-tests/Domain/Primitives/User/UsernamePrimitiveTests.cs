using System;
using blog_domain.Primitives;
using Xunit;

namespace blog_tests.Domain.Primitives.User;

public class UsernamePrimitiveTests
{
    [Fact]
    public void Rejects_empty_or_whitespace_strings()
    {
        // Arrange
        var invalidUsername = "    ";

        // Act
        Action act = () => Username.Create(invalidUsername);

        // Assert
        Assert.Throws<InvalidUsernameException>(act);
    }

    [Fact]
    public void Rejects_too_short_usernames()
    {
        // Arrange
        var shortUsername = "ab";

        // Act
        Action act = () => Username.Create(shortUsername);

        // Assert
        Assert.Throws<InvalidUsernameException>(act);
    }
}
