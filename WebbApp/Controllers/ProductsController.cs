using Microsoft.AspNetCore.Mvc;
using WebbApp.Repositories;
using WebbApp.Services;
using WebbApp.ViewModels;

namespace WebbApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;
        private readonly CategoryRepo _categoryRepo;
        public ProductsController(ProductService productService, CategoryRepo categoryRepo)
        {
            _productService = productService;
            _categoryRepo = categoryRepo;
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
        
        public IActionResult Cart()
        {
            ViewData["Title"] = "Your Cart";
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            ViewData["Title"] = "Details";

            var product = await _productService.GetAsync(x => x.ArticleNumber == id);

            return View(product);
        }
    }
}
