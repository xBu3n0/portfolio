using blog_api.Services.Blog.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace blog_api.Services.Blog;

[ApiController]
[Route("[controller]")]
public class BlogController : ControllerBase
{
    [HttpGet("posts")]
    public async Task<IResult> Store(CreatePostRequest request)
    {
        return Results.BadRequest(new { error = "Not implemented yet." });
    }
}
