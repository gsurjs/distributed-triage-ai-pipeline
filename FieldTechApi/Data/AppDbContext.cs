// bridge to SQLite database

using FieldTechApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FieldTechApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // This property represents the table in your database
    public DbSet<FieldReport> Reports => Set<FieldReport>();
}