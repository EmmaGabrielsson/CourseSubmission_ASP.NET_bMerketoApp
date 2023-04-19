using Microsoft.AspNetCore.Mvc;

namespace WebbApp.Controllers
{
    public class ProductsController : Controller
    {
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
        public IActionResult Cart()
        {
            ViewData["Title"] = "Cart";
            return View();
        }
        public IActionResult Details()
        {
            ViewData["Title"] = "Details";
            return View();
        }
    }
}
