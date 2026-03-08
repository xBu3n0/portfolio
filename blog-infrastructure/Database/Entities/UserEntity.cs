using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using blog_domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace blog_infrastructure.Database.Entities;

[Table("users")]
[Index(nameof(Username), IsUnique = true)]
public sealed class UserEntity
{
    private UserEntity()
    {
    }

    public UserEntity(Guid id, string username, string email, DateTime createdAt)
    {
        Id = id;
        Username = username;
        Email = email;
        CreatedAt = createdAt;
    }

    [Key]
    [Column("id", TypeName = "uuid")]
    public Guid Id { get; init; }

    [Required]
    [MaxLength(32)]
    [Column("username", TypeName = "character varying(32)")]
    public string Username { get; init; } = null!;

    [Required]
    [MaxLength(254)]
    [Column("email", TypeName = "character varying(254)")]
    public string Email { get; init; } = null!;

    [Required]
    [Column("created_at", TypeName = "timestamp with time zone")]
    public DateTime CreatedAt { get; init; }

    [InverseProperty(nameof(PostEntity.User))]
    public ICollection<PostEntity> Posts { get; init; } = new HashSet<PostEntity>();

    public User ToDomain()
        => User.Create(
            blog_domain.Primitives.UserId.Create(Id),
            blog_domain.Primitives.Username.Create(Username),
            blog_domain.Primitives.EmailAddress.Create(Email));
}
