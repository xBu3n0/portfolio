using System.Threading;
using System.Threading.Tasks;
using blog_domain.Primitives;

namespace blog_application.Ports;

public interface ICredentialRepository
{
    Task<string?> GetPasswordHashAsync(Username username, CancellationToken cancellationToken = default);
}
