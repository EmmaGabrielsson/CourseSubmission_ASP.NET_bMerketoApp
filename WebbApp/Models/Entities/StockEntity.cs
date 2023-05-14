using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

[PrimaryKey(nameof(ArticleNumber))]
public class StockEntity
{
    [Key]
    public string ArticleNumber { get; set; } = null!;
    public int Quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    public bool OnSale { get; set; } = false;
    public string StandardCurrency { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;
}