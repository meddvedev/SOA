using Microsoft.EntityFrameworkCore;

using API.Models.User;

namespace API.DBContext;

public class DBContext : DbContext
{
    private const string ConnectionString = "Host=localhost;Port=5432;Database=tax_dev;Username=tax;Password=tax";

    public DbSet<User> user { get; set; } = null!;

    public DBContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(ConnectionString);
    }
}

