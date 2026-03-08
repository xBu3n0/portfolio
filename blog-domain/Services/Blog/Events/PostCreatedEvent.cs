using System;
using blog_domain.Primitives;

namespace blog_domain.Services.Blog.Events;

public sealed record PostCreatedEvent(PostId PostId, UserId UserId, Title Title, Content Content)
    : DomainEvent(DomainEventName.PostCreated, DateTime.UtcNow);
