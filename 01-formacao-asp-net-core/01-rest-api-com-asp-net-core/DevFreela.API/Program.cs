using DevFreela.API.ExceptionHandler;
using DevFreela.Application;
using DevFreela.Application.Models;
using DevFreela.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<FreelanceTotalCostConfig>(
    builder.Configuration.GetSection("FreelanceTotalCostConfig")
    );

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Define a rota inicial
app.MapGet("/", (context) => { context.Response.Redirect("/scalar/v1"); return Task.CompletedTask; });

app.Run();