using WebbApp.Models.Entities;

namespace WebbApp.ViewModels;

public class GridCollectionViewModel
{
    public string Title { get; set; } = "";
    public IEnumerable<string> Categories { get; set; } = null!;
    public IEnumerable<GridCollectionItemViewModel> GridItems { get; set; } = null!;
    public bool LoadMore { get; set; } = false;

    public static implicit operator GridCollectionViewModel(CollectionEntity model)
    {
        return new GridCollectionViewModel
        {
            Title = model.Title!,
            //Categories = model.Categories,
            //GridItems = model.Products
        };
    }

}

