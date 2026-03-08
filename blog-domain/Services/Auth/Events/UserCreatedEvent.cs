using System;
using blog_domain.Events;
using blog_domain.Primitives;

namespace blog_domain.Services.Auth.Events;

public sealed record UserCreatedEvent(UserId UserId, Username Username, EmailAddress Email)
    : DomainEvent(DomainEventName.UserCreated, DateTime.UtcNow);
