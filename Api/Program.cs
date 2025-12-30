using Application.Products;
using Application.Users;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// سرویس‌ها
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("TestDb"));

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ProductService>();


// OpenAPI و Swagger برای Controllers
builder.Services.AddEndpointsApiExplorer(); // برای Minimal APIs
builder.Services.AddSwaggerGen();           // برای Controllers و تولید JSON OpenAPI

var app = builder.Build();

// JSON OpenAPI
app.UseSwagger(); // مسیر: /swagger/v1/swagger.json

// Redoc با CDN
app.MapGet("/docs", async context =>
{
    context.Response.ContentType = "text/html";
    await context.Response.WriteAsync(@"
        <!DOCTYPE html>
        <html>
          <head>
            <title>API Docs</title>
            <meta charset='UTF-8'>
            <script src='https://cdn.redoc.ly/redoc/latest/bundles/redoc.standalone.js'></script>
          </head>
          <body>
            <redoc spec-url='/swagger/v1/swagger.json'></redoc>
          </body>
        </html>
    ");
});

// Middleware های معمول
app.UseHttpsRedirection();
app.UseAuthorization();

try
{
    app.MapControllers();
}
catch (ReflectionTypeLoadException ex)
{
    foreach (var loaderException in ex.LoaderExceptions)
    {
        Console.WriteLine(loaderException.Message);
    }
    throw;
}

app.Run();
