using System;
using blog_domain.Entities;
using blog_domain.Primitives;
using Xunit;

namespace blog_tests.Domain.Entities.PostTests;

public class PostEntityTests
{
    [Fact]
    public void UpdateTitle_replaces_title_without_touching_content()
    {
        // Arrange
        var postId = PostId.Create(Guid.NewGuid());
        var authorId = UserId.Create(Guid.NewGuid());
        var initialTitle = Title.Create("Original Title");
        var initialContent = Content.Create("Original Content");
        var post = Post.Create(postId, authorId, initialTitle, initialContent);
        var updatedTitle = Title.Create("Updated Title");

        // Act
        post.UpdateTitle(updatedTitle);

        // Assert
        Assert.Equal(updatedTitle, post.Title);
        Assert.Equal(initialContent, post.Content);
        Assert.Equal(authorId, post.UserId);
    }

    [Fact]
    public void UpdateContent_replaces_content_without_touching_title()
    {
        // Arrange
        var postId = PostId.Create(Guid.NewGuid());
        var authorId = UserId.Create(Guid.NewGuid());
        var initialTitle = Title.Create("Original Title");
        var initialContent = Content.Create("Original Content");
        var post = Post.Create(postId, authorId, initialTitle, initialContent);
        var updatedContent = Content.Create("Updated Content");

        // Act
        post.UpdateContent(updatedContent);

        // Assert
        Assert.Equal(initialTitle, post.Title);
        Assert.Equal(updatedContent, post.Content);
        Assert.Equal(authorId, post.UserId);
    }
}
