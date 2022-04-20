using CsApiExample.Models;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("Connection");

builder.Services.AddControllers();
builder.Services.AddDbContext<CustomerContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
                .AddSqlServer(
                    connectionString, 
                    healthQuery: "SELECT 1", 
                    tags: new[] { "ready" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
    {
        Predicate = (_) => false
    });

    endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions
    {   
        Predicate = (check) => check.Tags.Contains("ready")
    });
});

app.Run();
