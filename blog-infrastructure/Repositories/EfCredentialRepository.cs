using System;
using System.Threading;
using System.Threading.Tasks;
using blog_application.Ports.Auth;
using blog_domain.Primitives;
using blog_infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace blog_infrastructure.Repositories;

public sealed class EfCredentialRepository : ICredentialRepository
{
    private readonly BlogDbContext _context;

    public EfCredentialRepository(BlogDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<string?> GetPasswordHashAsync(Username username, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Credentials
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.Username == username.Value, cancellationToken);

        return string.IsNullOrWhiteSpace(entity?.PasswordHash) ? null : entity.PasswordHash;
    }
}
