using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using WebbApp.Models.Entities;

namespace WebbApp.Models.ViewModels;

public class OrderRowViewModel
{
    public Guid OrderId { get; set; }
    public string ProductArticleNumber { get; set; } = null!;
    public string? ProductName { get; set; }
    public int Quantity { get; set; }

    [Column(TypeName = "money")]
    public decimal ProductPrice { get; set; }

    public static implicit operator OrderRowEntity(OrderRowViewModel model)
    {
        return new OrderRowEntity
        {
            OrderId = model.OrderId,
            ProductArticleNumber = model.ProductArticleNumber,
            Quantity = model.Quantity,
            ProductPrice = model.ProductPrice,
        };
    }
}
