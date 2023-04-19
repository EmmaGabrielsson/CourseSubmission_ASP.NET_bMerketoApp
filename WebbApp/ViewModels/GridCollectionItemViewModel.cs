using WebbApp.Models.Entities;

namespace WebbApp.ViewModels;

public class GridCollectionItemViewModel
{
    public string Id { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Description { get; set; }

    public bool OnSale = false;

    public static implicit operator ProductEntity(GridCollectionItemViewModel gridCollectionItemViewModel)
    {
        return new ProductEntity
        {
            ArticleNumber = gridCollectionItemViewModel.Id,
            ImageUrl = gridCollectionItemViewModel.ImageUrl,
            Title = gridCollectionItemViewModel.Title,
            Price = gridCollectionItemViewModel.Price,
            OnSale = gridCollectionItemViewModel.OnSale,
            Description = gridCollectionItemViewModel.Description
        };
    }
}
