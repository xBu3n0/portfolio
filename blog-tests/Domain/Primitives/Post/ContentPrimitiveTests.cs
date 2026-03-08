using System;
using blog_domain.Primitives;
using Xunit;

namespace blog_tests.Domain.Primitives.Post;

public class ContentPrimitiveTests
{
    [Fact]
    public void Rejects_empty_content()
    {
        // Arrange
        var emptyContent = string.Empty;

        // Act
        Action act = () => Content.Create(emptyContent);

        // Assert
        Assert.Throws<InvalidContentException>(act);
    }

    [Fact]
    public void Rejects_excessively_long_content()
    {
        // Arrange
        var longContent = new string('B', 10001);

        // Act
        Action act = () => Content.Create(longContent);

        // Assert
        Assert.Throws<InvalidContentException>(act);
    }
}
