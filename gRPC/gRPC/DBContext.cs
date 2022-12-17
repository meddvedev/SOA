using Microsoft.EntityFrameworkCore;

using gRPC.Models;

namespace gRPC.DBContext;

public class DBContext : DbContext
{
    private const string ConnectionString = "Host=localhost;Port=5432;Database=tax_dev;Username=tax;Password=tax";

    public DbSet<Models.Insurance> insurance { get; set; } = null!;

    public DBContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
    }
}

