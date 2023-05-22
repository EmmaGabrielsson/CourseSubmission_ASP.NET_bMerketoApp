using WebbApp.Models.Entities;

namespace WebbApp.Models.Dtos;

public class Order
{
    public Guid? Id { get; set; }
    public int? TotalQuantity { get; set; }
    public decimal? TotalPrice { get; set; }
    public ICollection<OrderRow>? OrderRows { get; set; } = new List<OrderRow>();

    public static implicit operator Order(OrderEntity entity)
    {
        return new Order
        {
            Id = entity.Id,
            TotalQuantity = entity.TotalQuantity,
            TotalPrice = entity.TotalPrice,
            OrderRows = (ICollection<OrderRow>)entity.OrderRows
        };
    }
}
