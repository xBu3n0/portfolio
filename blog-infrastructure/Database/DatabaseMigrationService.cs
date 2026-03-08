using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace blog_infrastructure.Database;

public interface IDatabaseMigrationService
{
    Task MigrateAsync(CancellationToken cancellationToken = default);
}

public sealed class DatabaseMigrationService : IDatabaseMigrationService
{
    private readonly BlogDbContext _context;

    public DatabaseMigrationService(BlogDbContext context)
    {
        _context = context;
    }

    public Task MigrateAsync(CancellationToken cancellationToken = default)
    {
        return _context.Database.MigrateAsync(cancellationToken);
    }
}
