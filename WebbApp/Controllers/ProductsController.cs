using Microsoft.AspNetCore.Mvc;
using WebbApp.Models.Dtos;
using WebbApp.Models.ViewModels;
using WebbApp.Repositories;
using WebbApp.Services;

namespace WebbApp.Controllers;

public class ProductsController : Controller
{
    #region Constructors & Private Fields
    private readonly ProductService _productService;
    private readonly CategoryRepo _categoryRepo;
    private readonly StockRepo _stockRepo;
    private readonly OrderService _orderService;

    public ProductsController(ProductService productService, CategoryRepo categoryRepo, StockRepo stockRepo, OrderService orderService)
    {
        _productService = productService;
        _categoryRepo = categoryRepo;
        _stockRepo = stockRepo;
        _orderService = orderService;
    }

    //private readonly OrderRepo _orderRepo;
    //private readonly OrderRowRepo _orderRowRepo;
    //private readonly IHttpContextAccessor _httpContextAccessor;
    #endregion
    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "All Products";
        ViewBag.AllProducts = await _productService.GetAllAsync();

        return View();
    }

    public async Task<IActionResult> Category(int id)
    {
        ViewData["Title"] = "Category";

        if(id == 0)
        {
            return View(new CategoryViewModel
            {
                Category = "on sale",
                Products = await _productService.GetAllOnSaleItemsAsync()
            });
        }
        var products = await _productService.GetAllCategoryProductsAsync(id);
        var category = await _categoryRepo.GetDataAsync(x => x.Id == id);
        var categoryName = category.CategoryName;

        var viewModel = new CategoryViewModel
        {
            Category = categoryName,
            Products = products
        };

        return View(viewModel);
    }
    public IActionResult Search()
    {
        ViewData["Title"] = "Search for products";
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Search(SearchViewModel searchModel)
    {
        ViewData["Title"] = "Search for products";

        if (ModelState.IsValid)
        {
            var searchedProducts = await _productService.GetAllSearchedAsync(searchModel);
            if (searchedProducts.SearchResults != null)
                return View(searchedProducts);
        }
        return View(searchModel);
    }

    public async Task<IActionResult> Details(string id)
    {
        ViewData["Title"] = "Details";

        Product product = await _productService.GetAsync(x => x.ArticleNumber == id);
        var productStock = await _stockRepo.GetDataAsync(x => x.ArticleNumber == product.ArticleNumber);

        if (productStock != null)
        {
            product.OnSale = productStock.OnSale;
            product.Price = productStock.Price;
            product.StandardCurrency = productStock.StandardCurrency;
            product.StockQuantity = productStock.Quantity;
        }
        if (product.StockQuantity == 0)
            product.ProductQuantity = 0;

        product.Reviews = await _productService.GetReviewsAsync(product.ArticleNumber!);
        product.Categories = await _productService.GetProductCategoriesListAsync(product.ArticleNumber!);
        product.Tags = await _productService.GetProductTagsListAsync(product.ArticleNumber!);

        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Details(Product product)
    {
        ViewData["Title"] = "Details";
        Product updatedProduct = await _productService.GetAsync(x => x.ArticleNumber == product.ArticleNumber);
        var productStock = await _stockRepo.GetDataAsync(x => x.ArticleNumber == updatedProduct.ArticleNumber);

        if (productStock != null)
        {
            updatedProduct.OnSale = productStock.OnSale;
            updatedProduct.Price = productStock.Price;
            updatedProduct.StandardCurrency = productStock.StandardCurrency;
            updatedProduct.StockQuantity = productStock.Quantity;
        }

        updatedProduct.Reviews = await _productService.GetReviewsAsync(updatedProduct.ArticleNumber!);
        updatedProduct.Categories = await _productService.GetProductCategoriesListAsync(updatedProduct.ArticleNumber!);
        updatedProduct.Tags = await _productService.GetProductTagsListAsync(updatedProduct.ArticleNumber!);
        updatedProduct.ProductQuantity = product.ProductQuantity;

        var order = await _orderService.GetOrCreateOrderAndAddRowsAsync(updatedProduct);

        if(order != null)
            return RedirectToAction("cart");

        return View(updatedProduct);
    }

    public async Task<IActionResult> Cart()
    {
            ViewData["Title"] = "Your Cart";
            Order order = await _orderService.GetOrderAsync();
            if (order != null)
            {
                var rows = await _orderService.GetOrderRowsAsync(x => x.OrderId == order.Id);
                order.OrderRows = (ICollection<OrderRow>?)rows;
                return View(order);
            }
        return View(order);
    }
}
