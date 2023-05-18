using WebbApp.Models.Entities;

namespace WebbApp.Models.ViewModels;

public class AllProductsViewModel
{
    public IEnumerable<GridCollectionItemViewModel> Products { get; set; } = null!;
}
