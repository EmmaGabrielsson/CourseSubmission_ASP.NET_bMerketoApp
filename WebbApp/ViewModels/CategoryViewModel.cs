namespace WebbApp.ViewModels;

public class CategoryViewModel
{
    public string Category { get; set; } = null!;
    public IEnumerable<GridCollectionItemViewModel> Products { get; set; } = null!;
}
