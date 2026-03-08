using System;
using System.Threading;
using System.Threading.Tasks;
using blog_application.Ports.Blog;
using blog_domain.Services.Blog.Entities;
using blog_infrastructure.Database;
using blog_infrastructure.Database.Entities;

namespace blog_infrastructure.Repositories;

public sealed class EfPostRepository : IPostRepository
{
    private readonly BlogDbContext _context;

    public EfPostRepository(BlogDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Post> AddAsync(Post post, CancellationToken cancellationToken = default)
    {
        var entity = new PostEntity(
            post.Id.Value,
            post.UserId.Value,
            post.Title.Value,
            post.Content.Value,
            DateTime.UtcNow);

        _context.Posts.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.ToDomain();
    }
}
