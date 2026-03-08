using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace blog_infrastructure.Database.Entities;

[Table("credentials")]
public sealed class CredentialEntity
{
    private CredentialEntity()
    {
    }

    public CredentialEntity(string username, string passwordHash, DateTime createdAt)
    {
        Username = username;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
    }

    [Key]
    [Required]
    [MaxLength(32)]
    [Column("username", TypeName = "character varying(32)")]
    public string Username { get; init; } = null!;

    [Required]
    [Column("password_hash", TypeName = "text")]
    public string PasswordHash { get; init; } = null!;

    [Required]
    [Column("created_at", TypeName = "timestamp with time zone")]
    public DateTime CreatedAt { get; init; }
}
