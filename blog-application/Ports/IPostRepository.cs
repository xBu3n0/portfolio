using System.Threading;
using System.Threading.Tasks;
using blog_domain.Entities;

namespace blog_application.Ports;

public interface IPostRepository
{
    Task<Post> AddAsync(Post post, CancellationToken cancellationToken = default);
}
