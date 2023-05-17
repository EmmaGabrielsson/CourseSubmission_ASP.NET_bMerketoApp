using WebbApp.Models.Entities;

namespace WebbApp.ViewModels;

public class GridCollectionItemViewModel
{
    public string Id { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }

    public static implicit operator GridCollectionItemViewModel(ProductEntity product)
    {
        return new GridCollectionItemViewModel
        {
            Id = product.ArticleNumber,
            ImageUrl = product.ImageUrl,
            Title = product.ProductName,
        };
    }
}
