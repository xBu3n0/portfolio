using System;

namespace blog_domain.Events;

public abstract record DomainEvent(DomainEventName EventName, DateTime OccurredOn) : IDomainEvent;
