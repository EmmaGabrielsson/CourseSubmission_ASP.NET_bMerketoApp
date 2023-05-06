using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using WebbApp.Models.Entities;

namespace WebbApp.ViewModels;

public class ProductRegisterViewModel
{
    public string ArticleNumber { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    [DataType(DataType.Upload)]
    public IFormFile Image { get; set; } = null!;
    public decimal Price { get; set; }
    public bool OnSale { get; set; } = false;

    public ICollection<ProductCategoryEntity> Categories { get; set; } = new HashSet<ProductCategoryEntity>();


    public static implicit operator ProductEntity(ProductRegisterViewModel model)
    {
        var entity = new ProductEntity
        {
            ArticleNumber = model.ArticleNumber,
            Title = model.Title,
            Description = model.Description,
            ImageUrl = $"{model.ArticleNumber}_{model.Image.FileName}",
            Price = model.Price,
            OnSale = model.OnSale,
            Categories = model.Categories
        };

        return entity;
    }
}
