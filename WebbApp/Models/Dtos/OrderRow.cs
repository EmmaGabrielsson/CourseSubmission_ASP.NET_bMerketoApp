using WebbApp.Models.Entities;

namespace WebbApp.Models.Dtos;

public class OrderRow
{
    public Guid? OrderId { get; set; }
    public string? ProductArticleNumber { get; set; }
    public int? Quantity { get; set; }
    public decimal? ProductPrice { get; set; }
    public string? ProductName { get; set; }
    public Order? Order { get; set; }

    public static implicit operator OrderRow (OrderRowEntity entity)
    {
        return new OrderRow
        {
            OrderId = entity.OrderId,
            ProductArticleNumber = entity.ProductArticleNumber,
            Quantity = entity.Quantity,
            ProductPrice = entity.ProductPrice,
            Order = entity.Order
        };
    }
}
