using System;
using blog_domain.Primitives;
using Xunit;

namespace blog_tests.Domain.Primitives.Post;

public class TitlePrimitiveTests
{
    [Fact]
    public void Rejects_empty_title()
    {
        // Arrange
        var emptyTitle = string.Empty;

        // Act
        Action act = () => Title.Create(emptyTitle);

        // Assert
        Assert.Throws<InvalidTitleException>(act);
    }

    [Fact]
    public void Rejects_overly_long_title()
    {
        // Arrange
        var longTitle = new string('A', 501);

        // Act
        Action act = () => Title.Create(longTitle);

        // Assert
        Assert.Throws<InvalidTitleException>(act);
    }
}
