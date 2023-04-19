using Microsoft.EntityFrameworkCore;
using WebbApp.Contexts;
using WebbApp.Models.Entities;
using WebbApp.ViewModels;

namespace WebbApp.Services;

public class ProductService
{
    private readonly DataContext _context;
    public ProductService(DataContext context)
    {
        _context = context;
    }

    private readonly List<GridCollectionItemViewModel> _saleItems = new()
    {
        new GridCollectionItemViewModel { Id = "11", Title = "Apple watch collection", ImageUrl = "images/placeholders/369x310.svg", Price = 25, OnSale = true },
        new GridCollectionItemViewModel { Id = "12", Title = "Beauty collection", ImageUrl = "images/placeholders/369x310.svg", Price = 50, OnSale = true}
    };

    public List<GridCollectionItemViewModel> GetAllSaleItems()
    {
        //var saleList = _saleItems.ToList();
        return _saleItems;
        //return await _context.Products.Where(x => x.OnSale == true).ToListAsync();

    }

    private readonly List<GridCollectionItemViewModel> _items = new()
    {
        new GridCollectionItemViewModel() {Id = "20", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true},
        new GridCollectionItemViewModel() {Id = "21", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true},
        new GridCollectionItemViewModel() {Id = "22", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true},
        new GridCollectionItemViewModel() {Id = "23", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true},
        new GridCollectionItemViewModel() {Id = "24", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true},
        new GridCollectionItemViewModel() {Id = "25", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true},
        new GridCollectionItemViewModel() {Id = "26", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true}
    };

    public Task<List<GridCollectionItemViewModel>> GetTopSaleItems()
    {
        //return await _items.Where(x => x.OnSale == true).ToList();
        //return await _context.Products.Where(x => x.Categories == "popular").ToListAsync();
    }

}
