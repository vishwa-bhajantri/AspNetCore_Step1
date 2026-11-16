using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi(); //create the Documentation for the API

builder.Services.AddEndpointsApiExplorer(); // Add this line to enable API explorer for endpoints

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Map the OpenAPI endpoint for documentation

    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "My API Documentation";
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
    });

    app.MapScalarApiReference(); // Map the Scalar API reference endpoint for documentation
}

app.MapGet("/", () => "API is Running");

app.MapGet("/FirstRecord", () => "API First Record is - John Doe");

app.MapGet("FirstRecordWithParam/{name}", (string name) =>
    $"API First Record is - {name}");

app.MapGet("add/{num1}/{num2}", (int num1, int num2) =>
    $"API Add Result is - {num1 + num2}");

app.MapGet("multiply/{num1}/{num2}", (int num1, int num2) =>
{
    return $"API Multiply Result is - {num1 * num2}";
});

app.MapGet("/html", () =>
{
    var htmlContent = "<html><body><h1>Hello, World!</h1></body></html>";
    return Results.Content(htmlContent, "text/html");
});

app.MapGet("/htmlwithCSS", () =>
{
    var htmlContent = "<html><body><style>h1 { color: blue; }</style><h1>Hello, World!</h1></body></html>";
    return Results.Content(htmlContent, "text/html");
});

app.MapGet("/htmlwithJS", () =>
{
    var htmlContent = "<html><body><h1>Hello, World!</h1><script>alert('Hello, from JavaScript!');</script></body></html>";
    return Results.Content(htmlContent, "text/html");
});

app.MapGet("/image", () =>
    {
        var imagePath = "wwwroot/Images/images.jpg"; // Ensure the image is placed in the wwwroot folder

        var imageBytes = File.ReadAllBytes(imagePath); // Read or convert the image file as bytes
        return Results.File(imageBytes, "image/jpeg");
    });

app.Run();
