using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.ComponentModel.DataAnnotations;
using WebbApp.Models.Entities;

namespace WebbApp.Models.ViewModels;

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

    [Display(Name = "Currency*")]
    public string Currency { get; set; } = null!;


    [Display(Name = "Product Image*")]
    [DataType(DataType.Upload)]
    public IFormFile Image { get; set; } = null!;

    [Display(Name = "Price*")]
    public decimal Price { get; set; }

    [Display(Name = "Stock Quantity*")]
    [Required(ErrorMessage = "The Quantity value is invalid.")]
    public int Quantity { get; set; }


    [Display(Name = "On Sale")]
    public bool OnSale { get; set; } = false;


    public static implicit operator ProductEntity(ProductRegisterViewModel model)
    {
        var entity = new ProductEntity
        {
            ArticleNumber = model.ArticleNumber,
            ProductName = model.ProductName,
            Ingress = model.Ingress,
            VendorName = model.VendorName,
            Description = model.Description,
            ImageUrl = $"{Guid.NewGuid()}_{model.Image.FileName}",
        };

        return entity;
    }

    public static implicit operator StockEntity(ProductRegisterViewModel model)
    {
        var entity = new StockEntity
        {
            ArticleNumber = model.ArticleNumber,
            Price = model.Price,
            OnSale = model.OnSale,
            Quantity = model.Quantity,
            StandardCurrency = model.Currency
        };

        return entity;
    }

}
