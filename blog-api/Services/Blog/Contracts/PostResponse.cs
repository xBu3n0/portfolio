using System;
using Swashbuckle.AspNetCore.Annotations;

namespace blog_api.Services.Blog.Contracts;

public sealed record PostResponse(
    [SwaggerSchema(Description = "Unique identifier assigned to the post.")]
    Guid Id,
    [SwaggerSchema(Description = "Identifier of the user who created the post.")]
    Guid UserId,
    [SwaggerSchema(Description = "Title of the post.")]
    string Title,
    [SwaggerSchema(Description = "Full post content.")]
    string Content);
