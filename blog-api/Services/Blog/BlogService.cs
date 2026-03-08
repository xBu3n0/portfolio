using blog_api.Services.Blog.Contracts;
using blog_application.Commands.CreatePost;
using blog_domain.Entities;
using blog_domain.Primitives;

namespace blog_api.Services.Blog;

public sealed class BlogService
{
    private readonly BlogServiceContext _context;

    public BlogService(BlogServiceContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<Post?> CreatePostAsync(Guid userId, CreatePostRequest request, CancellationToken cancellationToken = default)
    {
        if (request is null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var title = Title.Create(request.Title);
        var content = Content.Create(request.Content);

        var handler = new CreatePostCommandHandler(_context.PostRepository, _context.MessageBus);
        var command = new CreatePostCommand(
            PostId.Create(Guid.NewGuid()),
            UserId.Create(userId),
            title,
            content);

        return handler.Handle(command, cancellationToken);
    }
}
