using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace blog_infrastructure.Database;

public sealed class BlogDbContextFactory : IDesignTimeDbContextFactory<BlogDbContext>
{
    public BlogDbContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("BLOG_DATABASE_CONNECTION")
            ?? "Host=localhost;Database=blog;Username=postgres;Password=postgres";

        var optionsBuilder = new DbContextOptionsBuilder<BlogDbContext>();
        var assemblyName = typeof(BlogDbContext).Assembly.GetName().Name ?? throw new InvalidOperationException();
        optionsBuilder.UseNpgsql(connectionString, options => options.MigrationsAssembly(assemblyName));

        return new BlogDbContext(optionsBuilder.Options);
    }
}
