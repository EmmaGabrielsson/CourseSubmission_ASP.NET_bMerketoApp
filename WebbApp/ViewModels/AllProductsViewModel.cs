using WebbApp.Models.Entities;

namespace WebbApp.ViewModels;

public class AllProductsViewModel
{
    public IEnumerable<GridCollectionItemViewModel> Products { get; set; } = null!;
}
