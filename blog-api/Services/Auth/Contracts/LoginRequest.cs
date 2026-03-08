using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace blog_api.Services.Auth.Contracts;

[SwaggerSchema(Description = "Credentials payload for establishing a session.")]
public sealed record LoginRequest(
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    [SwaggerSchema(Description = "Username used during registration.")]
    string Username,

    [Required]
    [SwaggerSchema(Description = "Password associated with the provided username.")]
    string Password);
