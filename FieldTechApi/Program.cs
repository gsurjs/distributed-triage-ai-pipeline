// ==============================================================================
// INTEGRATION: Minimal APIs & Dependency Injection
// PURPOSE: Bootstraps the web server, registers AppDbContext with the dependency 
//          injection container, and defines the HTTP endpoints. Transitions the 
//          API from an in-memory list to persistent SQLite database operations.
// ==============================================================================

using FieldTechApi.Data;
using FieldTechApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add the DbContext to the container, pointing to SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// --- Minimal API Endpoints using EF Core ---

// GET: Retrieve all field reports
app.MapGet("/api/reports", async (AppDbContext db) => 
    await db.Reports.ToListAsync())
   .WithName("GetReports");

// POST: Submit a new field report
app.MapPost("/api/reports", async (FieldReport report, AppDbContext db) =>
{
    report.Id = Guid.NewGuid();
    report.CreatedAt = DateTime.UtcNow;
    
    db.Reports.Add(report);
    await db.SaveChangesAsync();
    
    return Results.Created($"/api/reports/{report.Id}", report);
})
.WithName("CreateReport");

app.Run();