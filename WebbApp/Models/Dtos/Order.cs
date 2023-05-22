using WebbApp.Models.Entities;
using WebbApp.Models.ViewModels;

namespace WebbApp.Models.Dtos;

public class Order
{
    public Guid? Id { get; set; }
    public int? TotalQuantity { get; set; }
    public decimal? TotalPrice { get; set; }
    public ICollection<OrderRowViewModel>? OrderRows { get; set; } = new List<OrderRowViewModel>();

    public static implicit operator Order(OrderEntity entity)
    {
        return new Order
        {
            Id = entity.Id,
            TotalQuantity = entity.TotalQuantity,
            TotalPrice = entity.TotalPrice,
            OrderRows = (ICollection<OrderRowViewModel>)entity.OrderRows
        };
    }
}
