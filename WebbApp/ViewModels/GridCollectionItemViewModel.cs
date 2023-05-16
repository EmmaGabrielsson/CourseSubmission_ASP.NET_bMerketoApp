using WebbApp.Models.Entities;

namespace WebbApp.ViewModels;

public class GridCollectionItemViewModel
{
    public string Id { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Title { get; set; } = null!;
    public decimal Price { get; set; }
    public string? Ingress { get; set; }
    public string? Description { get; set; }
    public string? Currency { get; set; }

    public bool OnSale = false;

    public int StockQuantity { get; set; }
    public ICollection<ProductCategoryEntity> CategoryIds { get; set; } = null!;
    public ICollection<ProductReviewEntity> Reviews { get; set; } = new HashSet<ProductReviewEntity>();


    public static implicit operator GridCollectionItemViewModel(ProductEntity model)
    {
        return new GridCollectionItemViewModel
        {
            Id = model.ArticleNumber,
            ImageUrl = model.ImageUrl,
            Title = model.ProductName,
            Ingress = model.Ingress,
            Description = model.Description,
            CategoryIds = model.Categories,
            Reviews = model.Reviews
        };
    }
    public static implicit operator GridCollectionItemViewModel(StockEntity model)
    {
        return new GridCollectionItemViewModel
        {
            Id = model.ArticleNumber,
            Price = model.Price,
            OnSale = model.OnSale,
            Currency = model.StandardCurrency,
            StockQuantity = model.Quantity
        };
    }

}
