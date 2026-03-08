using System;
using blog_domain.Primitives;

namespace blog_domain.Events;

public sealed record UserCreatedEvent(UserId UserId, Username Username, EmailAddress Email)
    : DomainEvent(DomainEventName.UserCreated, DateTime.UtcNow);
