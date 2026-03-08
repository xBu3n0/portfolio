using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using blog_application.Ports.Core;
using blog_domain.Events;

namespace blog_infrastructure.Services;

public sealed class ConsoleMessageBus : IMessageBus
{
    public Task PublishAsync(IDomainEvent[] domainEvents, CancellationToken cancellationToken = default)
    {
        if (domainEvents is null || domainEvents.Length == 0)
        {
            return Task.CompletedTask;
        }

        foreach (var domainEvent in domainEvents.Where(@event => @event is not null))
        {
            Console.WriteLine($"[{DateTime.UtcNow:o}] {domainEvent.EventName}");
        }

        return Task.CompletedTask;
    }
}
