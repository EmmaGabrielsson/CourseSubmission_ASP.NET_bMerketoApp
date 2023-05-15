using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebbApp.Models.Entities;

[PrimaryKey(nameof(ArticleNumber))]
public class ProductReviewEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public int Rating { get; set; } 
    public string? Comment { get; set; }
    public string ArticleNumber { get; set;} = null!;

    public ProductEntity Product { get; set; } = null!;
}
