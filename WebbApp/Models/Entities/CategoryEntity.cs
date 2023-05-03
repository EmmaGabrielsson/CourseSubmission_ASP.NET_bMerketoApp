using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(80)")]
        public string CategoryName { get; set; } = null!;

        public ICollection<ProductCategoryEntity> Products { get; set; } = new HashSet<ProductCategoryEntity>();

    }
}
