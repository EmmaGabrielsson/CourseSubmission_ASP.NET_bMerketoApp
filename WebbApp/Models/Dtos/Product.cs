using WebbApp.Models.Entities;

namespace WebbApp.Models.Dtos;

public class Product
{
    public string ArticleNumber { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public string? Ingress { get; set; }
    public string? Description { get; set; }
    public string? VendorName { get; set; }
    public string ImageUrl { get; set; } = null!;
    public decimal? Price { get; set; }
    public bool OnSale { get; set; } = false;

    public ICollection<ProductCategoryEntity> Categories { get; set; } = new HashSet<ProductCategoryEntity>();
    public ICollection<ProductReviewEntity> Reviews { get; set; } = new HashSet<ProductReviewEntity>();

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
