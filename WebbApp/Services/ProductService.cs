using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

    public List<GridCollectionItemViewModel> GetAllOnSaleItems()
    {
        return _saleItems;
        //return await _context.Products.Where(x => x.OnSale == true).ToListAsync();

    }

    public GridCollectionViewModel BestCollection = new()
    {
        Title = "Best Collection",
        Categories = new List<string> { "All", "Bag", "Dress", "Decoration", "Essentials", "Interior", "Laptops", "Mobile", "Beauty" },
        GridItems = new List<GridCollectionItemViewModel>
                {
                    new GridCollectionItemViewModel { Id = "1", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "2", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "3", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "4", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "5", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "6", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "7", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 },
                    new GridCollectionItemViewModel { Id = "8", Title = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg\r\n", Price = 30 }
                }
    };

    public async Task CreateInitializedTopSaleProductAsync()
    {
        if (!await _context.Categories.AnyAsync())
        {
            await _context.AddAsync(new CategoryEntity { CategoryName = "new" });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new CategoryEntity { CategoryName = "popular" });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new CategoryEntity { CategoryName = "featured" });
            await _context.SaveChangesAsync();
        }

        if (!await _context.Products.AnyAsync())
        {
            await _context.AddAsync(new ProductEntity { ArticleNumber = "25695", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductEntity { ArticleNumber = "35685", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductEntity { ArticleNumber = "48952", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductEntity { ArticleNumber = "52365", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductEntity { ArticleNumber = "75214", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductEntity { ArticleNumber = "89652", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductEntity { ArticleNumber = "96174", Title = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.SaveChangesAsync();

            var popularCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == "popular");

            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "25695" });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "35685" });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "48952" });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "52365" });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "75214" });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "89652" });
            await _context.SaveChangesAsync();
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "96174" });
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<GridCollectionItemViewModel>> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();
        var newList = new List<GridCollectionItemViewModel>();
        foreach (var item in products)
        {
            newList.Add(item);
        }
        return newList;
    }

    public async Task<IEnumerable<GridCollectionItemViewModel>> GetAllTopSaleProductsAsync()
    {
        await CreateInitializedTopSaleProductAsync();
        var topSaleProducts = new List<GridCollectionItemViewModel>();
        var popularCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == "popular");
        var topSaleIds = await _context.ProductCategories.Where(x => x.CategoryId == popularCategory!.Id).ToListAsync();
        foreach (var id in topSaleIds)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ArticleNumber == id.ProductId);
            topSaleProducts.Add(product!);

        }

        return topSaleProducts!;
    }

    public async Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> predicate)
    {
        var item = await _context.Products.FirstOrDefaultAsync(predicate);

        if (item != null)
            return item;

        return null!;
    }
    public async Task<IEnumerable<GridCollectionItemViewModel>> GetAllSearchedAsync(string searchText)
    {
        var searchedProducts = new List<GridCollectionItemViewModel>();
        var findProducts = await _context.Products.Where(x => x.Title.Contains(searchText)).ToListAsync();
        foreach (var product in findProducts)
        {
            searchedProducts.Add(product!);
        }

        return searchedProducts!;
    }

}
