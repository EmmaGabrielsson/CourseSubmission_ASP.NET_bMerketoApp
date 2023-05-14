using System.ComponentModel.DataAnnotations;

namespace WebbApp.ViewModels;

public class SearchViewModel
{
    [Required(ErrorMessage = "You need to enter a text to be able to do a search")]
    public string SearchText { get; set; } = null!;

    public ICollection<GridCollectionItemViewModel> SearchResults { get; set; } = new List<GridCollectionItemViewModel>();

}
