namespace WebbApp.Models.Entities;

public class CollectionEntity
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public ICollection<ProductTagEntity> ProductIds { get; set; } = new HashSet<ProductTagEntity>();
}
