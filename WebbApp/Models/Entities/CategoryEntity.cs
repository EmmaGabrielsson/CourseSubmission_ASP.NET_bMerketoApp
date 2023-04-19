using System.ComponentModel.DataAnnotations.Schema;

namespace WebbApp.Models.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;

        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; } = null!;
        public ProductEntity Product { get; set; } = null!;
    }
}
