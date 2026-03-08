using System.Threading;
using System.Threading.Tasks;
using blog_domain.Services.Auth.Entities;
using blog_domain.Primitives;

namespace blog_application.Ports.Auth;

public interface IUserRepository
{
    Task<User> AddAsync(User user, CancellationToken cancellationToken = default);

    Task<User?> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default);
}
