using System.ComponentModel.DataAnnotations;

namespace WebbApp.Models.Entities;

public class StockEntity
{
    [Key]
    public string ArticleNumber { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string StandardCurrency { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;
}