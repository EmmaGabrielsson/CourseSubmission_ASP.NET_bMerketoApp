using Microsoft.AspNetCore.Mvc;
using WebbApp.Services;

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
        /*
        [HttpPost]
        public async Task<IActionResult> Search(string searchText)
        {
            ViewData["Title"] = "Searched Products";

            if (ModelState.IsValid)
            {
                if (await _productService.GetAllSearchedAsync(searchText))
                    ModelState.AddModelError("", "There is already a user with the same email address");
                else
                {
                    if (await _productService.GetAllSearchedAsync(searchText))
                        return RedirectToAction("Search", "Account");
                    else
                        ModelState.AddModelError("", "Something went wrong when trying to registrate user.");
                }
            }
            return View(searchText);
        }
        */
        public IActionResult Cart()
        {
            ViewData["Title"] = "Your Cart";
            return View();
        }
        public IActionResult Details()
        {
            ViewData["Title"] = "Details";
            return View();
        }
    }
}
