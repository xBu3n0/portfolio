using blog_api.Services.Auth;
using blog_api.Services.Blog;
using blog_application.Ports.Auth;
using blog_application.Ports.Blog;
using blog_application.Ports.Core;
using blog_application.Services.Auth;
using blog_infrastructure.Database;
using blog_infrastructure.Repositories;
using blog_infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());
builder.Services.AddControllers();

const string defaultConnection = "Host=localhost;Database=blog;Username=blogadmin;Password=blogsecret";
var connectionString = builder.Configuration["BLOG_DATABASE_CONNECTION"]
    ?? builder.Configuration.GetConnectionString("BlogDatabase")
    ?? defaultConnection;

builder.Services.AddDbContext<BlogDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddScoped<IDatabaseMigrationService, DatabaseMigrationService>();
builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<IPostRepository, EfPostRepository>();
builder.Services.AddScoped<ICredentialRepository, EfCredentialRepository>();
builder.Services.AddScoped<IMessageBus, ConsoleMessageBus>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();

builder.Services.AddScoped<AuthServiceContext>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<BlogServiceContext>();
builder.Services.AddScoped<BlogService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var migrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrationService>();
    await migrator.MigrateAsync();
}

app.MapControllers();

app.Run();
