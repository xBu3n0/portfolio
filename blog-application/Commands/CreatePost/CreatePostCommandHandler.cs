using blog_application.Ports;
using blog_domain.Entities;
using blog_domain.Events;

namespace blog_application.Commands.CreatePost;

public sealed class CreatePostCommandHandler
{
    private readonly IPostRepository _postRepository;
    private readonly IMessageBus _messageBus;

    public CreatePostCommandHandler(IPostRepository postRepository, IMessageBus messageBus)
    {
        _postRepository = postRepository;
        _messageBus = messageBus;
    }

    public Task<Post?> Handle(CreatePostCommand command, CancellationToken cancellationToken = default)
    {
        return _postRepository.AddAsync(Post.Create(command.PostId, command.UserId, command.Title, command.Content), cancellationToken)
            .ContinueWith(userTask =>
            {
                var post = userTask.Result;
                if (post is not null)
                {
                    _messageBus.PublishAsync(
                        [
                            new PostCreatedEvent(post.Id, post.UserId, post.Title, post.Content)
                        ],
                        cancellationToken
                    );
                }
                return post;
            }, cancellationToken);
    }
}
