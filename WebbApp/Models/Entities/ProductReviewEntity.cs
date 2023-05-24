using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

public class ProductReviewEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public int Rating { get; set; } 
    public string? Comment { get; set; }


    [ForeignKey(nameof(ProductArticleNumber))]
    public string ProductArticleNumber { get; set;} = null!;
    public ProductEntity Product { get; set; } = null!;
}
