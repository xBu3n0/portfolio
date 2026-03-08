using System;
using System.Threading;
using System.Threading.Tasks;
using blog_application.Ports.Auth;
using blog_domain.Services.Auth.Entities;
using blog_domain.Primitives;
using blog_infrastructure.Database;
using blog_infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace blog_infrastructure.Repositories;

public sealed class EfUserRepository : IUserRepository
{
    private readonly BlogDbContext _context;

    public EfUserRepository(BlogDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<User> AddAsync(User user, CancellationToken cancellationToken = default)
    {
        var entity = new UserEntity(
            user.Id.Value,
            user.Username.Value,
            user.Email.Value,
            DateTime.UtcNow);

        _context.Users.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.ToDomain();
    }

    public async Task<User?> GetByUsernameAsync(Username username, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Username == username.Value, cancellationToken);

        return entity?.ToDomain();
    }
}
