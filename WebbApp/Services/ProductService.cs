using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebbApp.Models.Dtos;
using WebbApp.Models.Entities;
using WebbApp.Models.ViewModels;
using WebbApp.Repositories;

namespace WebbApp.Services;

public class ProductService
{
    #region Constructors and Private Fields
    private readonly ProductRepo _productRepo;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly TagRepo _tagRepo;
    private readonly CategoryRepo _categoryRepo;
    private readonly ProductCategoryRepo _productCategoryRepo;
    private readonly ProductTagRepo _productTagRepo;
    private readonly StockRepo _stockRepo;
    private readonly ProductReviewRepo _productReviewRepo;
    private readonly CollectionRepo _collectionRepo;
    private readonly ShowcaseRepo _showcaseRepo;

    public ProductService(ProductRepo productRepo, IWebHostEnvironment webHostEnvironment, TagRepo tagRepo, CategoryRepo categoryRepo, ProductCategoryRepo productCategoryRepo, ProductTagRepo productTagRepo, StockRepo stockRepo, ProductReviewRepo productReviewRepo, CollectionRepo collectionRepo, ShowcaseRepo showcaseRepo)
    {
        _productRepo = productRepo;
        _webHostEnvironment = webHostEnvironment;
        _tagRepo = tagRepo;
        _categoryRepo = categoryRepo;
        _productCategoryRepo = productCategoryRepo;
        _productTagRepo = productTagRepo;
        _stockRepo = stockRepo;
        _productReviewRepo = productReviewRepo;
        _collectionRepo = collectionRepo;
        _showcaseRepo = showcaseRepo;
    }
    #endregion

    public async Task<ProductEntity> CreateAsync(ProductRegisterViewModel model)
    {
        var _entity = await _productRepo.GetDataAsync(x => x.ArticleNumber == model.ArticleNumber);
        if (_entity == null)
        {
            var product = await _productRepo.AddDataAsync(model);
            if (product != null)
            {
                var stock = await _stockRepo.AddDataAsync(model);
                if(stock != null)
                    return product;
            }
        }
        return null!;
    }
    public async Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> predicate)
    {
        try 
        {
            var _entity = await _productRepo.GetDataAsync(predicate);
            if (_entity != null)
                return _entity!;        
        } catch { return null!; }
        return null!;
    }
    public async Task<bool> UploadImageAsync(ProductEntity product, IFormFile image)
    {
        try
        {
            string imagePath = $"{_webHostEnvironment.WebRootPath}/images/products/{product.ImageUrl}";
            await image.CopyToAsync(new FileStream(imagePath, FileMode.Create));
            return true;
        }
        catch { return false; }
    }
    public async Task<List<GridCollectionItemViewModel>> GetAllOnSaleItemsAsync()
    {
        List<StockEntity> onSaleProducts = (List<StockEntity>)await _stockRepo.GetAllDataAsync(x => x.OnSale == true);
        var list = new List<GridCollectionItemViewModel>();
        if (onSaleProducts != null)
        {
            foreach (var item in onSaleProducts)
            {
                var product = await _productRepo.GetDataAsync(x => x.ArticleNumber == item.ProductArticleNumber);
                list.Add(product);
            }

            return list;

        }
        return null!;
    }
    public async Task<ShowcaseEntity> GetLatestShowcaseAsync()
    {
        try
        {
            var showcases = await _showcaseRepo.GetAllDataAsync();

            if (showcases != null)
                return showcases.OrderByDescending(x => x.CreatedDate).FirstOrDefault()!;

        return null!;
        } catch { return null!; }
    }
    public async Task<IEnumerable<GridCollectionItemViewModel>> GetAllAsync()
    {
        try
        {
            var products = await _productRepo.GetAllDataAsync();
            var newList = new List<GridCollectionItemViewModel>();
            foreach (var item in products)
                newList.Add(item);

            return newList;
        } catch { return null!; }
    }
    public async Task<IEnumerable<GridCollectionItemViewModel>> GetAllCategoryProductsAsync(int categoryId)
    {
        try
        {
            var categoryProducts = new List<GridCollectionItemViewModel>();
            var productIds = await _productCategoryRepo.GetAllDataAsync(x => x.CategoryId == categoryId);
            foreach (var id in productIds)
            {
                var product = await _productRepo.GetDataAsync(x => x.ArticleNumber == id.ProductId);
                if (product != null)
                    categoryProducts.Add(product);
            }
            return categoryProducts!;

        } catch { return null!; }
    }
    public async Task<List<ProductReviewEntity>> GetReviewsAsync(string articleNumber)
    {
        try
        {
            var reviews = await _productReviewRepo.GetAllDataAsync(x => x.ProductArticleNumber == articleNumber);
            return (List<ProductReviewEntity>)reviews;
        } catch { return null!; }
    }
    public async Task<List<CategoryEntity>> GetProductCategoriesListAsync(string articleNumber)
    {
        try
        {
            var categories = new List<CategoryEntity>();
            var productIds = await _productCategoryRepo.GetAllDataAsync(x => x.ProductId == articleNumber);
            
            if (productIds != null)
            {
                foreach (var id in productIds)
                {
                    var category = await _categoryRepo.GetDataAsync(x => x.Id == id.CategoryId);
                    if (category != null)
                        categories.Add(category);
                }

                return categories;
            }
            return null!;
        }
        catch {  return null!; }
    }
    public async Task<List<TagEntity>> GetProductTagsListAsync(string articleNumber)
    {
        try
        {
            var tags = new List<TagEntity>();
            var productIds = await _productTagRepo.GetAllDataAsync(x => x.ProductId == articleNumber); 
            
            if (productIds != null)
            {
                foreach (var id in productIds)
                {
                    var tag = await _tagRepo.GetDataAsync(x => x.Id == id.TagId); 
                    if (tag != null)
                        tags.Add(tag);
                }
                return tags;
            }
            return null!;
        } catch { return null!; }
    }
    public async Task<GridCollectionViewModel> GetBestCollectionAsync()
    {
        try
        {
            var bestCollection = await _collectionRepo.GetDataAsync(x => x.Title == "best collection");
            var collection = new GridCollectionViewModel();
            if (bestCollection != null)
            {
                collection.Title = bestCollection.Title!;
                var featured = await _tagRepo.GetDataAsync(x => x.TagName == "featured");

                if (featured != null)
                {
                    IEnumerable<ProductTagEntity> productIds = await _productTagRepo.GetAllDataAsync(x => x.TagId == featured.Id);

                    if( productIds != null)
                    {
                        foreach(var item in productIds)
                        {
                            var product = await _productRepo.GetDataAsync(x => x.ArticleNumber == item.ProductId);
                            if(product != null)
                                collection.GridItems!.Add(product);
                        }

                    }
                        return collection;
                }
            }
            return null!;
        } catch { return null!; }
    }
    public async Task<GridCollectionViewModel> GetTopSaleCollectionAsync()
    {
        try
        {
            var topSaleCollection = await _collectionRepo.GetDataAsync(x => x.Title == "top selling products in this week");
            var collection = new GridCollectionViewModel();
            if (topSaleCollection != null)
            {
                collection.Title = topSaleCollection.Title!;
                var popular = await _tagRepo.GetDataAsync(x => x.TagName == "popular");

                if (popular != null)
                {
                    IEnumerable<ProductTagEntity> productIds = await _productTagRepo.GetAllDataAsync(x => x.TagId == popular.Id);

                    if (productIds != null)
                    {
                        foreach (var item in productIds)
                        {
                            var product = await _productRepo.GetDataAsync(x => x.ArticleNumber == item.ProductId);
                            if (product != null)
                                collection.GridItems!.Add(product);
                        }

                    }
                    return collection;
                }
            }
            return null!;
        }
        catch { return null!; }

    }
    public async Task<SearchViewModel> GetAllSearchedAsync(SearchViewModel searchModel)
    {
        try
        {
            var searchedProducts = new List<GridCollectionItemViewModel>();
            var findProducts = await _productRepo.GetAllDataAsync(x => x.ProductName.Contains(searchModel.SearchText));
            
            foreach (var product in findProducts)
            {
                searchedProducts.Add(product!);
            }
            searchModel.SearchResults = searchedProducts;
            return searchModel!;

        } catch { return null!; }
    }
    public async Task<List<SelectListItem>> GetTagsAsync()
    {
        var tags = new List<SelectListItem>();

        foreach (var tag in await _tagRepo.GetAllDataAsync())
        {
            tags.Add(new SelectListItem
            {
                Value = tag.Id.ToString(),
                Text = tag.TagName,
            });
        }
        return tags;
    }
    public async Task<List<SelectListItem>> GetTagsAsync(string[] selectedTags)
    {
        var tags = new List<SelectListItem>();

        foreach (var tag in await _tagRepo.GetAllDataAsync())
        {
            tags.Add(new SelectListItem{
                Value = tag.Id.ToString(),
                Text = tag.TagName,
                Selected = selectedTags.Contains(tag.Id.ToString())
            });
        }
        return tags;
    }
    public async Task<List<SelectListItem>> GetCategoriesAsync()
    {
        var categories = new List<SelectListItem>();

        foreach (var category in await _categoryRepo.GetAllDataAsync())
        {
            categories.Add(new SelectListItem
            {
                Value = category.Id.ToString(),
                Text = category.CategoryName,
            });
        }
        return categories;
    }
    public async Task<List<SelectListItem>> GetCategoriesAsync(string[] selectedTags)
    {
        var categories = new List<SelectListItem>();

        foreach (var category in await _categoryRepo.GetAllDataAsync())
        {
            categories.Add(new SelectListItem
            {
                Value = category.Id.ToString(),
                Text = category.CategoryName,
                Selected = selectedTags.Contains(category.Id.ToString())
            });
        }
        return categories;
    }
    public async Task<bool> AddProductTagsAsync(ProductEntity product, string[] tags)
    {
        try
        {
            foreach (var tag in tags)
            {
                await _productTagRepo.AddDataAsync(new ProductTagEntity
                {
                    ProductId = product.ArticleNumber,
                    TagId = int.Parse(tag)
                });
            }
            return true;
        }
        catch { return false; }
    }
    public async Task<bool> AddProductCategoriesAsync(ProductEntity product, string[] categories)
    {
        try
        {
            foreach (var category in categories)
            {
                await _productCategoryRepo.AddDataAsync(new ProductCategoryEntity
                {
                    ProductId = product.ArticleNumber,
                    CategoryId = int.Parse(category)
                });
            }
            return true;
        }
        catch { return false; }
    }

}
