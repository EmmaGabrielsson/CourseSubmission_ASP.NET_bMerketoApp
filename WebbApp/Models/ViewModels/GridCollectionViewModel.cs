using WebbApp.Models.Entities;

namespace WebbApp.Models.ViewModels;

public class GridCollectionViewModel
{
    public string Title { get; set; } = "";
    public List<CategoryEntity>? Categories { get; set; }
    public ICollection<GridCollectionItemViewModel>? GridItems { get; set; } = new HashSet<GridCollectionItemViewModel>();
    public bool LoadMore { get; set; } = false;

}

