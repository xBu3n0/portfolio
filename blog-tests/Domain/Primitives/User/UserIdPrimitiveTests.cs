using System;
using blog_domain.Primitives;
using Xunit;

namespace blog_tests.Domain.Primitives.User;

public class UserIdPrimitiveTests
{
    [Fact]
    public void Rejects_empty_guid()
    {
        // Arrange
        var emptyGuid = Guid.Empty;

        // Act
        Action act = () => UserId.Create(emptyGuid);

        // Assert
        Assert.Throws<InvalidUserIdException>(act);
    }

    [Fact]
    public void Accepts_non_empty_guid()
    {
        // Arrange
        var realGuid = Guid.NewGuid();

        // Act
        var userId = UserId.Create(realGuid);

        // Assert
        Assert.Equal(realGuid, userId.Value);
    }
}
