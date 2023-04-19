using Microsoft.AspNetCore.Mvc;

namespace WebbApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
