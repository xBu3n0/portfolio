using blog_application.Services.Blog.Commands.CreatePost;
using blog_application.Ports.Blog;
using blog_application.Ports.Core;
using blog_domain.Services.Blog.Entities;
using blog_domain.Events;
using blog_domain.Services.Blog.Events;
using blog_domain.Primitives;
using Moq;

namespace blog_tests.Application.Services.Blog.Commands.CreatePost;

public class CreatePostCommandHandlerTests
{
    [Fact]
    public async Task Handle_persists_post_and_dispatches_event()
    {
        // Arrange
        var postRepository = new Mock<IPostRepository>();
        var messageBus = new Mock<IMessageBus>();
        var handler = new CreatePostCommandHandler(postRepository.Object, messageBus.Object);

        var newPost = Post.Create(
            PostId.Create(Guid.NewGuid()),
            UserId.Create(Guid.NewGuid()),
            Title.Create("Integration testing is fun"),
            Content.Create("Text stored by the repository")
        );

        postRepository.Setup(repo => repo.AddAsync(
            It.IsAny<Post>(),
            It.IsAny<CancellationToken>()))
        .ReturnsAsync(newPost);

        IDomainEvent[] publishedEvents = Array.Empty<IDomainEvent>();
        messageBus.Setup(bus => bus.PublishAsync(
            It.IsAny<IDomainEvent[]>(),
            It.IsAny<CancellationToken>()))
        .Callback<IDomainEvent[], CancellationToken>((events, _) => publishedEvents = events)
        .Returns(Task.CompletedTask);

        var command = new CreatePostCommand(
            newPost.Id,
            newPost.UserId,
            newPost.Title,
            newPost.Content
        );

        // Act
        var createdPost = await handler.Handle(command, CancellationToken.None);

        // Assert
        postRepository.Verify(repo => repo.AddAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()), Times.Once);
        messageBus.Verify(bus => bus.PublishAsync(It.IsAny<IDomainEvent[]>(), It.IsAny<CancellationToken>()), Times.Once);

        Assert.NotNull(createdPost);
        Assert.Equal(command.PostId, newPost.Id);
        Assert.Equal(command.UserId, newPost.UserId);
        Assert.Equal(command.Title, newPost.Title);
        Assert.Equal(command.Content, newPost.Content);

        Assert.Contains(publishedEvents, e => e is PostCreatedEvent);
    }
}
