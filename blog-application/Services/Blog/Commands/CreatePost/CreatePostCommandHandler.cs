using blog_application.Ports.Blog;
using blog_application.Ports.Core;
using blog_domain.Services.Blog.Entities;
using blog_domain.Services.Blog.Events;

namespace blog_application.Services.Blog.Commands.CreatePost;

public sealed class CreatePostCommandHandler
{
    private readonly IPostRepository _postRepository;
    private readonly IMessageBus _messageBus;

    public CreatePostCommandHandler(IPostRepository postRepository, IMessageBus messageBus)
    {
        _postRepository = postRepository;
        _messageBus = messageBus;
    }

    public async Task<Post> Handle(CreatePostCommand command, CancellationToken cancellationToken = default)
    {
        var post = await _postRepository.AddAsync(Post.Create(command.PostId, command.UserId, command.Title, command.Content), cancellationToken);

        if (post is null)
        {
            throw new InvalidOperationException("Failed to create post.");
        }

        await _messageBus.PublishAsync(
            [
                new PostCreatedEvent(post.Id, post.UserId, post.Title, post.Content)
            ],
            cancellationToken
        );

        return post;
    }
}
