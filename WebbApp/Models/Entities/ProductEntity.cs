using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

public class ProductEntity
{
    [Key]
    public string ArticleNumber { get; set; } = null!;

    [Column(TypeName = "nvarchar(100)")]
    public string ProductName { get; set; } = null!;
    public string? Ingress { get; set; }
    public string? Description { get; set; }
    public string? VendorName { get; set; }
    public string ImageUrl { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal? Price { get; set; }
    public bool OnSale { get; set; } = false;

    public ICollection<ProductTagEntity> Tags { get; set; } = new HashSet<ProductTagEntity>();
    public ICollection<ProductCategoryEntity> Categories { get; set; } = new HashSet<ProductCategoryEntity>();
    public ICollection<ProductReviewEntity> Reviews { get; set; } = new HashSet<ProductReviewEntity>();
}

