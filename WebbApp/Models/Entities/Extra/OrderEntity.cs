using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities.Extra;

public class OrderEntity
{
    public Guid Id { get; set; }
    public int TotalQuantity { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalPrice { get; set; }
    public ICollection<OrderRowEntity> Products { get; set; } = new HashSet<OrderRowEntity>();

}
