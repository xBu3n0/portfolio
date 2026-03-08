using System;

namespace blog_domain.Events;

public interface IDomainEvent
{
    DomainEventName EventName { get; }

    DateTime OccurredOn { get; }
}
