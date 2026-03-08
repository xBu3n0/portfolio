using System;
using blog_domain.Primitives;

namespace blog_domain.Entities;

public sealed class Post
{
    public PostId Id { get; }

    public UserId UserId { get; }

    public Title Title { get; private set; }

    public Content Content { get; private set; }

    private Post(PostId id, UserId userId, Title title, Content content)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        UserId = userId ?? throw new ArgumentNullException(nameof(userId));
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Content = content ?? throw new ArgumentNullException(nameof(content));
    }

    public static Post Create(PostId id, UserId userId, Title title, Content content)
        => new(id, userId, title, content);

    public void UpdateTitle(Title title)
    {
        if (title is null)
        {
            throw new ArgumentNullException(nameof(title));
        }

        Title = title;
    }

    public void UpdateContent(Content content)
    {
        if (content is null)
        {
            throw new ArgumentNullException(nameof(content));
        }

        Content = content;
    }
}
