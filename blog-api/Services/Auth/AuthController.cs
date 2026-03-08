using blog_api.Services.Auth.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Services.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IResult> Register(CreateUserRequest request)
    {
        return Results.BadRequest(new { error = "Not implemented yet." });
    }

    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequest request)
    {
        return Results.BadRequest(new { error = "Not implemented yet." });
    }
}
