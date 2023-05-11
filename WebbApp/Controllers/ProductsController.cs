using Microsoft.AspNetCore.Mvc;
using WebbApp.Services;
using WebbApp.ViewModels;

namespace WebbApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "All Products";
            return View();
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
                if (searchedProducts.SearchResults == null)
                    ModelState.AddModelError("", $"We have no products that match your search for {searchModel.SearchText}");
                else
                {
                    //return RedirectToAction("SearchedProducts", "Products");
                    return View(searchedProducts);
                }
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
