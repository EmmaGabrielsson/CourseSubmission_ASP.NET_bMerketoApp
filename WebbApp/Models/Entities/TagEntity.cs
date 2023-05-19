using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities;

public class TagEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(80)")]
    public string TagName { get; set; } = null!;
    public ICollection<ProductTagEntity> ProductTags { get; set; } = new HashSet<ProductTagEntity>();


}
