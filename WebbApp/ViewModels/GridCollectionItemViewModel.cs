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


    public bool OnSale = false;

    public List<string> Categories { get; set; } = null!;
    
    public static implicit operator GridCollectionItemViewModel(ProductEntity model)
    {
        return new GridCollectionItemViewModel
        {
            Id = model.ArticleNumber,
            ImageUrl = model.ImageUrl,
            Title = model.ProductName,
            //Price = model.Price,
            //OnSale = model.OnSale,
            Ingress = model.Ingress,
            Description = model.Description,
        };
    }

}
