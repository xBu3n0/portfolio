using System;
using Swashbuckle.AspNetCore.Annotations;

namespace blog_api.Services.Auth.Contracts;

public sealed record UserResponse(
    [SwaggerSchema(Description = "Unique identifier assigned to the user.")]
    Guid Id,
    [SwaggerSchema(Description = "Username chosen by the user.")]
    string Username,
    [SwaggerSchema(Description = "Email address tied to the account.")]
    string Email);
