using Microsoft.EntityFrameworkCore;

namespace CarvedRock.Admin.Data;

public class ProductDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();

    public string DbPath { get; set; }

    public ProductDbContext()
    {
        var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        DbPath = Path.Join(path, "carved-rock.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
}