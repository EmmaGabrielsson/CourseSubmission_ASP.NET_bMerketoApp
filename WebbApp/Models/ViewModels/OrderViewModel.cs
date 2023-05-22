using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using WebbApp.Models.Entities;

namespace WebbApp.Models.ViewModels;

public class OrderViewModel
{
    public Guid Id { get; set; }
    public int TotalQuantity { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalPrice { get; set; }
    public ICollection<OrderRowViewModel> OrderRows { get; set; } = new List<OrderRowViewModel>();

    public static implicit operator OrderEntity(OrderViewModel model)
    {
        return new OrderEntity
        {
            Id = model.Id,
            TotalQuantity = model.TotalQuantity,
            TotalPrice = model.TotalPrice,
            OrderRows = (ICollection<OrderRowEntity>)model.OrderRows
        };
    }
}
