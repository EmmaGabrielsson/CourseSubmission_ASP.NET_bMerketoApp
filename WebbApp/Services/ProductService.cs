using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebbApp.Contexts;
using WebbApp.Models.Dtos;
using WebbApp.Models.Entities;
using WebbApp.Repositories;
using WebbApp.ViewModels;

namespace WebbApp.Services;

public class ProductService
{
    private readonly DataContext _context;
    private readonly ProductRepo _productRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductService(DataContext context, ProductRepo productRepo, IWebHostEnvironment webHostEnvironment)
    {
        _context = context;
        _productRepo = productRepo;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<Product> CreateAsync(ProductEntity entity)
    {
        var _entity = await _productRepo.GetDataAsync(x => x.ArticleNumber == entity.ArticleNumber);
        if (_entity == null)
        {
            await _productRepo.AddDataAsync(entity);
            if(_entity != null)
                return _entity;
        }
        return null!;
    }
    public async Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> predicate)
    {
        var _entity = await _productRepo.GetDataAsync(predicate);
        if (_entity != null)
        {
            return _entity!;
        }
        return null!;
    }
    public async Task<bool> UploadImageAsync(Product product, IFormFile image)
    {
        try
        {
            string imagePath = $"{_webHostEnvironment.WebRootPath}/images/products/{product.ImageUrl}";
            await image.CopyToAsync(new FileStream(imagePath, FileMode.Create));
            return true;
        }
        catch { return false; }
    }
   public async Task<IEnumerable<ProductEntity>> GetAllOnSaleItems()
    {
        IEnumerable<ProductEntity> onSaleProducts = await _context.Products.Where(x => x.OnSale == true).ToListAsync();
        return onSaleProducts;
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
    public async Task CreateInitializedDataAsync()
    {
        if (!await _context.Tags.AnyAsync())
        {
            await _context.AddAsync(new TagEntity { TagName = "new" });
            await _context.AddAsync(new TagEntity { TagName = "popular" });
            await _context.AddAsync(new TagEntity { TagName = "featured" });
            await _context.AddAsync(new TagEntity { TagName = "summer sale" });
            await _context.AddAsync(new TagEntity { TagName = "outgoing" });
            await _context.SaveChangesAsync();
        }

        if (!await _context.Categories.AnyAsync())
        {
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
            await _context.SaveChangesAsync();
        }

        if (!await _context.Products.AnyAsync())
        {
            await _context.AddAsync(new ProductEntity { ArticleNumber = "S1132", ProductName = "Apple watch collection", ImageUrl = "369x310.svg", Price = 25, OnSale = true });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "S8812", ProductName = "Apple watch collection", ImageUrl = "369x310.svg", Price = 50, OnSale = true });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "25695", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Price = 5, OnSale = false, Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "35685", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Price = 5, OnSale = false, Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "48952", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Price = 5, OnSale = false, Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "52365", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Price = 5, OnSale = false, Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "75214", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Price = 5, OnSale = false, Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "89652", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Price = 5, OnSale = false, Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.AddAsync(new ProductEntity { ArticleNumber = "96174", ProductName = "Beauty collection", ImageUrl = "270x295.svg", Price = 5, OnSale = false, Ingress = "Discover our exquisite beauty collection, where elegance meets innovation. Unleash your radiance with our curated selection of high-quality products that elevate your skincare and makeup routine. From luxurious serums to captivating cosmetics, each item is designed to enhance your natural beauty. Explore our range and embark on a transformative journey to reveal your inner glow. Indulge in our carefully crafted collection and experience the power of self-care. Shop now and embrace the beauty that lies within you." });
            await _context.SaveChangesAsync();

            var popularTag = await _context.Tags.FirstOrDefaultAsync(x => x.TagName == "popular");
            var summerSaleTag = await _context.Tags.FirstOrDefaultAsync(x => x.TagName == "summer sale");
            var allCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == "all");
            var beautyCategory = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == "beauty");

            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "25695" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "35685" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "48952" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "52365" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "75214" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "89652" });
            await _context.AddAsync(new ProductTagEntity { TagId = popularTag!.Id, ProductId = "96174" });
            await _context.AddAsync(new ProductTagEntity { TagId = summerSaleTag!.Id, ProductId = "S1132" });
            await _context.AddAsync(new ProductTagEntity { TagId = summerSaleTag!.Id, ProductId = "S8812" });
            await _context.SaveChangesAsync();

            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "S8812" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "S1132" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "96174" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "89652" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "75214" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "52365" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "48952" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "35685" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = beautyCategory!.Id, ProductId = "25695" });
            
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "S8812" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "S1132" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "96174" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "89652" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "75214" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "52365" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "48952" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "35685" });
            await _context.AddAsync(new ProductCategoryEntity { CategoryId = allCategory!.Id, ProductId = "25695" });
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
    /*
    public async Task<List<GridCollectionItemViewModel>> GetAllAsync(int skip, int take)
    {
        var products = await _context.Products
            .OrderBy(x => x.ProductName)
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        var newList = new List<GridCollectionItemViewModel>();
        foreach (var item in products)
        {
            newList.Add(item);
        }
        return newList;
    }
    */

    public async Task<IEnumerable<GridCollectionItemViewModel>> GetAllTopSaleProductsAsync()
    {
        await CreateInitializedDataAsync();
        var topSaleProducts = new List<GridCollectionItemViewModel>();
        var popularTag = await _context.Tags.FirstOrDefaultAsync(x => x.TagName == "popular");
        var topSaleIds = await _context.ProductTags.Where(x => x.TagId == popularTag!.Id).ToListAsync();
        foreach (var id in topSaleIds)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ArticleNumber == id.ProductId);
            topSaleProducts.Add(product!);

        }

        return topSaleProducts!;
    }
    public async Task<IEnumerable<GridCollectionItemViewModel>> GetAllCategoryProductsAsync(int categoryId)
    {
        var categoryProducts = new List<GridCollectionItemViewModel>();
        var productIds = await _context.ProductCategories.Where(x => x.CategoryId == categoryId).ToListAsync();
        foreach (var id in productIds)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.ArticleNumber == id.ProductId);
            categoryProducts.Add(product!);
        }

        return categoryProducts!;
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
