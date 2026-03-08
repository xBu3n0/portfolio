using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace blog_api.Services.Auth.Contracts;

[SwaggerSchema(Description = "Payload for registering a new user.")]
public sealed record CreateUserRequest(
    [Required]
    [MinLength(3)]
    [MaxLength(20)]
    [SwaggerSchema(Description = "Unique username between 3 and 20 characters.")]
    string Username,

    [Required]
    [EmailAddress]
    [SwaggerSchema(Description = "Valid email address for the user.")]
    string Email);
