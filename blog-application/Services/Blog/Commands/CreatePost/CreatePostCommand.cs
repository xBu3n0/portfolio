using blog_domain.Primitives;

namespace blog_application.Services.Blog.Commands.CreatePost;

public sealed record CreatePostCommand(PostId PostId, UserId UserId, Title Title, Content Content);
