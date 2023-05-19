using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

[PrimaryKey(nameof(ProductId), nameof(TagId))]
public class ProductTagEntity
{
    [ForeignKey(nameof(Product))]
    public string ProductId { get; set; } = null!;
    public ProductEntity Product { get; set; } = null!;

    [ForeignKey(nameof(Tag))]
    public int TagId { get; set; }
    public TagEntity Tag { get; set; } = null!;

    public int? CollectionId { get; set; }
    public CollectionEntity? Collection { get; set; }

}

