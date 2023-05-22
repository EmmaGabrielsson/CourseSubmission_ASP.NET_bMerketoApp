using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

[PrimaryKey(nameof(OrderId), nameof(ProductArticleNumber))]
public class OrderRowEntity
{
    [ForeignKey(nameof(Order))]
    public Guid OrderId { get; set; }
    public string ProductArticleNumber { get; set; } = null!;
    public int Quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal ProductPrice { get; set; }
    public OrderEntity Order { get; set; } = null!;

}
