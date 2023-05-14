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
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
    public DbSet<ShowcaseEntity> Showcases { get; set; }
    public DbSet<ProductReviewEntity> ProductReviews { get; set; }
    public DbSet<StockEntity> Stocks { get; set; }
    public DbSet<CollectionEntity> Collections { get; set; }
}
