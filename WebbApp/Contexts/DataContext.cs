using Microsoft.EntityFrameworkCore;
using WebbApp.Models.Entities;

namespace WebbApp.Contexts;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ProductEntity> Products { get; set; }

}
