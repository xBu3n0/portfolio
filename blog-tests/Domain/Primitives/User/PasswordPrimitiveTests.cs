using System;
using blog_domain.Primitives;
using Xunit;

namespace blog_tests.Domain.Primitives.User;

public class PasswordPrimitiveTests
{
    [Fact]
    public void Rejects_short_values()
    {
        // Arrange
        var shortPassword = "abc";

        // Act
        Action act = () => Password.Create(shortPassword);

        // Assert
        Assert.Throws<WeakPasswordException>(act);
    }

    [Fact]
    public void Rejects_missing_complexity()
    {
        // Arrange
        var simplePassword = "aaaaaaaa";

        // Act
        Action act = () => Password.Create(simplePassword);

        // Assert
        Assert.Throws<WeakPasswordException>(act);
    }
}
