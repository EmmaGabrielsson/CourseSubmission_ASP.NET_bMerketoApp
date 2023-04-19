using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

public class ProductEntity
{
    [Key]
    public string ArticleNumber { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
    public bool OnSale { get; set; } = false;

    public ICollection<CategoryEntity> Categories { get; set; } = null!;

}
