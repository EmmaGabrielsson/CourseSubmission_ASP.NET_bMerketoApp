
namespace WebbApp.ViewModels;

public class TopSellingViewModel
{
    public IEnumerable<GridCollectionItemViewModel> SaleItems { get; set; } = null!;
    public bool LoadMore { get; set; } = false;

}
