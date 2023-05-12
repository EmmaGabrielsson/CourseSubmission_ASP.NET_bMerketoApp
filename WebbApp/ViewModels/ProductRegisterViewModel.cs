using System.ComponentModel.DataAnnotations;
using WebbApp.Models.Entities;

namespace WebbApp.ViewModels;

public class ProductRegisterViewModel
{
    [Display(Name = "Article Number*")]
    public string ArticleNumber { get; set; } = null!;

    [Display(Name = "Product Name*")]
    public string ProductName { get; set; } = null!;

    [Display(Name = "Ingress")]
    public string? Ingress { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }

    [Display(Name = "Vendor Name")]
    public string? VendorName { get; set; }

    [Display(Name = "Product Image*")]
    [DataType(DataType.Upload)]
    public IFormFile Image { get; set; } = null!;

    [Display(Name = "Price*")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Display(Name = "Stock Quantity*")]
    public int Quantity { get; set; }


    [Display(Name = "On Sale")]
    public bool OnSale { get; set; } = false;

    public ICollection<int> CategoryIds { get; set; } = null!;


    public static implicit operator ProductEntity(ProductRegisterViewModel model)
    {
        var entity = new ProductEntity
        {
            ArticleNumber = model.ArticleNumber,
            ProductName = model.ProductName,
            Ingress = model.Ingress,
            VendorName = model.VendorName,
            Description = model.Description,
            ImageUrl = $"{model.ArticleNumber}_{model.Image.FileName}",
            Price = model.Price,
            OnSale = model.OnSale,
        };

        return entity;
    }
}
