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

app.Run();
