using Microsoft.AspNetCore.Mvc;
using WebbApp.Models.Dtos;
using WebbApp.Models.Entities;
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
    private readonly OrderRepo _orderRepo;
    private readonly OrderRowRepo _orderRowRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ProductsController(ProductService productService, CategoryRepo categoryRepo, StockRepo stockRepo, OrderRepo orderRepo, OrderRowRepo orderRowRepo, IHttpContextAccessor httpContextAccessor)
    {
        _productService = productService;
        _categoryRepo = categoryRepo;
        _stockRepo = stockRepo;
        _orderRepo = orderRepo;
        _orderRowRepo = orderRowRepo;
        _httpContextAccessor = httpContextAccessor;
    }
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

        // Check if the order ID exists in the session
        string orderId = _httpContextAccessor.HttpContext!.Session.GetString("OrderId")!;
        if (orderId == null)
        {
            orderId = Guid.NewGuid().ToString();
            _httpContextAccessor.HttpContext.Session.SetString("OrderId", orderId);
        }
        OrderEntity order = await _orderRepo.GetDataAsync(x => x.Id == Guid.Parse(orderId));
        OrderRowEntity orderRow = await _orderRowRepo.GetDataAsync(x => x.OrderId == Guid.Parse(orderId) && x.ProductArticleNumber == updatedProduct.ArticleNumber);
        
        if (order != null)
        {
            order.TotalQuantity += updatedProduct.ProductQuantity;
            order.TotalPrice += (decimal)(updatedProduct.Price * updatedProduct.ProductQuantity)!;
            await _orderRepo.UpdateDataAsync(order);
        }
        else
        {
            order = new OrderEntity
            {
                Id = Guid.Parse(orderId),
                TotalQuantity = updatedProduct.ProductQuantity,
                TotalPrice = (decimal)(updatedProduct.Price * updatedProduct.ProductQuantity)!
            };
            await _orderRepo.AddDataAsync(order);
        }
        
        if (orderRow != null)
        {
            orderRow.Quantity = product.ProductQuantity;
            await _orderRowRepo.UpdateDataAsync(orderRow);
        }
        else
        {
            orderRow = new OrderRowEntity
            {
                OrderId = Guid.Parse(orderId),
                ProductArticleNumber = updatedProduct.ArticleNumber!,
                ProductPrice = (decimal)updatedProduct.Price!,
                Quantity = product.ProductQuantity
            };
            await _orderRowRepo.AddDataAsync(orderRow);
        }

        return RedirectToAction("cart");

        //return View(updatedProduct);
    }

    public async Task<IActionResult> Cart()
    {
            ViewData["Title"] = "Your Cart";
            string orderId = _httpContextAccessor.HttpContext!.Session.GetString("OrderId")!;
            /*    
            if (orderId == null)
            {
                orderId = Guid.NewGuid().ToString();
                _httpContextAccessor.HttpContext.Session.SetString("OrderId", orderId);
            }
            */
            Order order = await _orderRepo.GetDataAsync(x => x.Id == Guid.Parse(orderId));
            if (order != null)
            {
                order.OrderRows = (ICollection<OrderRowViewModel>?)await _orderRowRepo.GetAllDataAsync(x => x.OrderId == order.Id);
                return View(order);
            /*
                var orderView = new Order
                {
                    Id = order.Id,
                    TotalPrice = order.TotalPrice,
                    TotalQuantity = order.TotalQuantity,
                    OrderRows = (ICollection<OrderRowViewModel>)await _orderRowRepo.GetAllDataAsync(x => x.OrderId == order.Id)
                };
            */
            }
        else
        {
            return View(order);
        }
    }
}
