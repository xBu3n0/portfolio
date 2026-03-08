using blog_application.Ports;

namespace blog_api.Services.Blog;

public sealed class BlogServiceContext
{
    public BlogServiceContext(IPostRepository postRepository, IMessageBus messageBus)
    {
        PostRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        MessageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
    }

    public IPostRepository PostRepository { get; }

    public IMessageBus MessageBus { get; }
}
