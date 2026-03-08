using System;
using blog_domain.Entities;
using blog_domain.Primitives;
using Xunit;

namespace blog_tests.Domain.Entities.UserTests;

public class UserEntityTests
{
    [Fact]
    public void ChangeEmail_updates_the_registered_email()
    {
        // Arrange
        var userId = UserId.Create(Guid.NewGuid());
        var username = Username.Create("valid-user");
        var initialEmail = EmailAddress.Create("first@example.com");
        var user = User.Create(userId, username, initialEmail);
        var newEmail = EmailAddress.Create("second@example.com");

        // Act
        user.ChangeEmail(newEmail);

        // Assert
        Assert.Equal(newEmail, user.Email);
    }
}
