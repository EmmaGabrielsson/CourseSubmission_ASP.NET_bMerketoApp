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

    public async Task<bool> CreateAsync(ProductEntity entity)
    {
        var _entity = await GetAsync(x => x.ArticleNumber == entity.ArticleNumber);
        if (_entity == null)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }

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
            await _context.AddAsync(new CategoryEntity { CategoryName = "popular" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "featured" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "all" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "bag" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "dress" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "decoration" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "essentials" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "interior" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "laptops" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "mobile" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "beauty" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "electronics" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "light" });
            await _context.AddAsync(new CategoryEntity { CategoryName = "summer" });
            await _context.SaveChangesAsync();
        }

        if (!await _context.Products.AnyAsync())
        {
            await _context.AddAsync(new ProductEntity { ArticleNumber = "25695", ProductName = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "35685", ProductName = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "48952", ProductName = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "52365", ProductName = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "75214", ProductName = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "89652", ProductName = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "96174", ProductName = "Beauty collection", ImageUrl = "images/placeholders/270x295.svg", Price = 5, OnSale = true });
            await _context.SaveChangesAsync();

            var popularCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == "popular");

            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "25695" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "35685" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "48952" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "52365" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "75214" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = popularCategory!.Id, ProductId = "89652" });
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
    /*
    public async Task<bool> CreateInitializedBestCollectionAsync()
    {
        if (!await _context.Collections.AnyAsync())
        {
            var featuredCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == "featured");

            await _context.AddAsync (new ProductEntity { ArticleNumber = "f-25695", ProductName = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg", Price = 30 });
            await _context.AddAsync (new ProductEntity { ArticleNumber = "f-35685", ProductName = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg", Price = 30 });
            await _context.AddAsync (new ProductEntity { ArticleNumber = "f-48952", ProductName = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg", Price = 30 });
            await _context.AddAsync (new ProductEntity { ArticleNumber = "f-52365", ProductName = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg", Price = 30 });
            await _context.AddAsync (new ProductEntity { ArticleNumber = "f-75214", ProductName = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg", Price = 30 });
            await _context.AddAsync (new ProductEntity { ArticleNumber = "f-89652", ProductName = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg", Price = 30 });
            await _context.AddAsync (new ProductEntity { ArticleNumber = "f-96174", ProductName = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg", Price = 30 });
            await _context.AddAsync (new ProductEntity { ArticleNumber = "f-06174", ProductName = "Apple watch collection", ImageUrl = "images/placeholders/270x295.svg", Price = 30 });                        
            await _context.SaveChangesAsync();

            await _context.AddAsync(new ProductCategoryEntity { CategoryId = featuredCategory!.Id, ProductId = "f-25695" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = featuredCategory!.Id, ProductId = "f-35685" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = featuredCategory!.Id, ProductId = "f-48952" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = featuredCategory!.Id, ProductId = "f-52365" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = featuredCategory!.Id, ProductId = "f-75214" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = featuredCategory!.Id, ProductId = "f-89652" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = featuredCategory!.Id, ProductId = "f-96174" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = featuredCategory!.Id, ProductId = "f-06174" });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new CollectionEntity
            {
                Title = "best collection",
                Categories = new List<string> { "All", "Bag", "Dress", "Decoration", "Essentials", "Interior", "Laptops", "Mobile", "Beauty" },
                Products = (ICollection<ProductEntity>)await _context.ProductCategories.Where(x => x.CategoryId == featuredCategory!.Id).ToListAsync()
            });
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
    public async Task<GridCollectionViewModel> GetBestCollectionAsync()
    {
        await CreateInitializedBestCollectionAsync();
        var bestCollection = new GridCollectionViewModel();
        var featuredProducts = new List<GridCollectionItemViewModel>();
        var featuredCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == "featured");
        var featuredItemIds= await _context.ProductCategories.Where(x => x.CategoryId == featuredCategory!.Id).ToListAsync();
        
        foreach (var id in featuredItemIds)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ArticleNumber == id.ProductId);
            featuredProducts.Add(product!);

        }

        return bestCollection;
    }
    */
    public async Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> predicate)
    {
        var item = await _context.Products.FirstOrDefaultAsync(predicate);

        if (item != null)
            return item;

        return null!;
    }

    public async Task<SearchViewModel> GetAllSearchedAsync(SearchViewModel searchModel)
    {
        var searchedProducts = new List<GridCollectionItemViewModel>();
        var findProducts = await _context.Products.Where(x => x.ProductName.Contains(searchModel.SearchText)).ToListAsync();
        foreach (var product in findProducts)
        {
            searchedProducts.Add(product!);
        }
        searchModel.SearchResults = searchedProducts;
        return searchModel!;
    }

}
