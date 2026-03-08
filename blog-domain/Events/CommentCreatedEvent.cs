using System;
using blog_domain.Primitives;

namespace blog_domain.Events;

public sealed record CommentCreatedEvent(CommentId CommentId, PostId PostId, UserId AuthorId, Content Content, CommentId? ParentCommentId = null)
    : DomainEvent(DomainEventName.CommentCreated, DateTime.UtcNow);
