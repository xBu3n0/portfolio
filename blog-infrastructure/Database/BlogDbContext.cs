using blog_infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace blog_infrastructure.Database;

public sealed class BlogDbContext : DbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options)
        : base(options)
    {
    }

    public DbSet<PostEntity> Posts => Set<PostEntity>();

    public DbSet<UserEntity> Users => Set<UserEntity>();

    public DbSet<CredentialEntity> Credentials => Set<CredentialEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PostEntity>(builder =>
        {
            builder.ToTable("posts");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired().HasMaxLength(256);
            builder.Property(p => p.Content).IsRequired();
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.UserId).IsRequired();
        });

        modelBuilder.Entity<UserEntity>(builder =>
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Username).IsRequired().HasMaxLength(32);
            builder.HasIndex(u => u.Username).IsUnique();
            builder.Property(u => u.Email).IsRequired().HasMaxLength(254);
            builder.Property(u => u.CreatedAt).IsRequired();
        });

        modelBuilder.Entity<CredentialEntity>(builder =>
        {
            builder.ToTable("credentials");
            builder.HasKey(c => c.Username);
            builder.Property(c => c.Username).IsRequired().HasMaxLength(32);
            builder.Property(c => c.PasswordHash).IsRequired();
            builder.Property(c => c.CreatedAt).IsRequired();
        });
    }
}
