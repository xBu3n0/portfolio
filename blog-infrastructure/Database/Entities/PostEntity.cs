using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using blog_domain.Services.Blog.Entities;
using Microsoft.EntityFrameworkCore;

namespace blog_infrastructure.Database.Entities;

[Table("posts")]
[Index(nameof(UserId))]
public sealed class PostEntity
{
    private PostEntity()
    {
    }

    public PostEntity(Guid id, Guid userId, string title, string content, DateTime createdAt)
    {
        Id = id;
        UserId = userId;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
    }

    [Key]
    [Column("id", TypeName = "uuid")]
    public Guid Id { get; init; }

    [Required]
    [Column("user_id", TypeName = "uuid")]
    public Guid UserId { get; init; }

    [Required]
    [MaxLength(256)]
    [Column("title", TypeName = "character varying(256)")]
    public string Title { get; init; } = null!;

    [Required]
    [Column("content", TypeName = "text")]
    public string Content { get; init; } = null!;

    [Required]
    [Column("created_at", TypeName = "timestamp with time zone")]
    public DateTime CreatedAt { get; init; }

    [ForeignKey(nameof(UserId))]
    public UserEntity? User { get; init; }

    public Post ToDomain()
        => Post.Create(
            blog_domain.Primitives.PostId.Create(Id),
            blog_domain.Primitives.UserId.Create(UserId),
            blog_domain.Primitives.Title.Create(Title),
            blog_domain.Primitives.Content.Create(Content));
}
