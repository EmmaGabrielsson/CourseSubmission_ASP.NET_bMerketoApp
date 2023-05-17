using WebbApp.Models.Entities;

namespace WebbApp.Models.Dtos;

public class Product
{
    public string? ArticleNumber { get; set; }
    public string? ProductName { get; set; }
    public string? Ingress { get; set; }
    public string? Description { get; set; }
    public string? VendorName { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? Price { get; set; }
    public bool? OnSale { get; set; }
    public int? StockQuantity { get; set; }
    public string? StandardCurrency { get; set; }

    public List<TagEntity> Tags { get; set; } = new List<TagEntity>();
    public List<CategoryEntity> Categories { get; set; } = new List<CategoryEntity>();
    public List<ProductReviewEntity> Reviews { get; set; } = new List<ProductReviewEntity>();


    public static implicit operator Product(ProductEntity entity)
    {
        var product = new Product
        {
            ArticleNumber = entity.ArticleNumber,
            ProductName = entity.ProductName,
            Ingress = entity.Ingress,
            VendorName = entity.VendorName,
            Description = entity.Description,
            ImageUrl = entity.ImageUrl,
        };
        return product;
    }
}
