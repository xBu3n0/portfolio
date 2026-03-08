using System;
using blog_domain.Primitives;
using Xunit;

namespace blog_tests.Domain.Services.Blog.Primitives.Post;

public class PostIdPrimitiveTests
{
    [Fact]
    public void Rejects_empty_guid()
    {
        // Arrange
        var emptyGuid = Guid.Empty;

        // Act
        Action act = () => PostId.Create(emptyGuid);

        // Assert
        Assert.Throws<InvalidPostIdException>(act);
    }

    [Fact]
    public void Accepts_non_empty_guid()
    {
        // Arrange
        var realGuid = Guid.NewGuid();

        // Act
        var postId = PostId.Create(realGuid);

        // Assert
        Assert.Equal(realGuid, postId.Value);
    }
}
