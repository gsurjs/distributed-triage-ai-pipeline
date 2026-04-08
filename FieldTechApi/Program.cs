using FieldTechApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container for Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Temporary in-memory data store for Phase 1
var reports = new List<FieldReport>();

// --- Minimal API Endpoints ---

// GET: Retrieve all field reports
app.MapGet("/api/reports", () => Results.Ok(reports))
   .WithName("GetReports");

// POST: Submit a new field report
app.MapPost("/api/reports", (FieldReport report) =>
{
    report.Id = Guid.NewGuid();
    report.CreatedAt = DateTime.UtcNow;
    reports.Add(report);
    
    return Results.Created($"/api/reports/{report.Id}", report);
})
.WithName("CreateReport");

app.Run();