using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

public class OrderEntity
{
    public Guid Id { get; set; }
    public DateTime? Created { get; set; } = DateTime.Now;
    public string? UserId { get; set; }
    public int TotalQuantity { get; set; }

    [Column(TypeName = "money")]
    public decimal TotalPrice { get; set; }
    public bool CheckOut { get; set; } = false;
    public ICollection<OrderRowEntity> OrderRows { get; set; } = new HashSet<OrderRowEntity>();

}
