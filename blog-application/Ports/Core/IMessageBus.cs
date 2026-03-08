using System.Threading;
using System.Threading.Tasks;
using blog_domain.Events;

namespace blog_application.Ports.Core;

public interface IMessageBus
{
    Task PublishAsync(IDomainEvent[] domainEvents, CancellationToken cancellationToken = default);
}
