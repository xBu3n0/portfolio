using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace blog_api.Services.Blog.Contracts;

[SwaggerSchema(Description = "Details required to publish a new post.")]
public sealed record CreatePostRequest(
    [Required]
    [SwaggerSchema(Description = "Identifier of the author (GUID string).")]
    string UserId,

    [Required]
    [MinLength(1)]
    [MaxLength(256)]
    [SwaggerSchema(Description = "Post title up to 256 characters.")]
    string Title,

    [Required]
    [MinLength(1)]
    [MaxLength(10000)]
    [SwaggerSchema(Description = "Body text of the post, up to 10,000 characters.")]
    string Content);
