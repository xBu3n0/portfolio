using System.Threading;
using System.Threading.Tasks;
using blog_domain.Services.Blog.Entities;

namespace blog_application.Ports.Blog;

public interface IPostRepository
{
    Task<Post> AddAsync(Post post, CancellationToken cancellationToken = default);
}
