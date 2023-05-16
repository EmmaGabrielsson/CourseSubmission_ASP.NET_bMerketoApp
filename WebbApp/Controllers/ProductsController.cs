using Microsoft.AspNetCore.Mvc;
using WebbApp.Models.Dtos;
using WebbApp.Repositories;
using WebbApp.Services;
using WebbApp.ViewModels;

namespace WebbApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryRepo _categoryRepo;
        private readonly StockRepo _stockRepo;
        public ProductsController(ProductService productService, CategoryRepo categoryRepo, StockRepo stockRepo)
        {
            _productService = productService;
            _categoryRepo = categoryRepo;
            _stockRepo = stockRepo;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "All Products";
            return View();
        }

        public async Task<IActionResult> Category(int id)
        {
            ViewData["Title"] = "Category";
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

            return View(product);
        }
        public IActionResult Cart()
        {
            ViewData["Title"] = "Your Cart";
            return View();
        }
    }
}
