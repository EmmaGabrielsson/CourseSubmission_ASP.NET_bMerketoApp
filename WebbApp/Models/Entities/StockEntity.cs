using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

[PrimaryKey(nameof(ProductArticleNumber))]
public class StockEntity
{
    [ForeignKey(nameof(Product))]
    public string ProductArticleNumber { get; set; } = null!;
    public int Quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    public bool OnSale { get; set; } = false;

    [Column(TypeName = "money")]
    public decimal Discount { get; set; } = decimal.Zero;
    public string StandardCurrency { get; set; } = "USD";
    public ProductEntity Product { get; set; } = null!;
}