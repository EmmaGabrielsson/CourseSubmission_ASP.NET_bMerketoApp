using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebbApp.Services;
using WebbApp.ViewModels;

namespace WebbApp.Controllers;

[Authorize(Roles = "admin")]
public class AdminController : Controller
{
    private readonly UserService _userService;
    private readonly ProductService _productService;
    public AdminController(UserService userService, ProductService productService)
    {
        _userService = userService;
        _productService = productService;
    }

    public IActionResult Index()
    {
        ViewData["Title"] = "Admin - Dashboard";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(UpdateRoleViewModel model)
    {
        ViewData["Title"] = "Admin - Dashboard";

        if (ModelState.IsValid)
        {
            var update = await _userService.UpdateUserRoleAsync(model);
            if (update)
                return RedirectToAction("index", "admin");

            ModelState.AddModelError("", "Something went wrong when trying to change users role. Make sure to choose a role and enter a valid user-id.");
        }

        return View(model);
    }

    public IActionResult Products()
    {
        ViewData["Title"] = "Admin - Add Products";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Products(ProductRegisterViewModel model)
    {
        ViewData["Title"] = "Admin - Add Products";

        if (ModelState.IsValid)
        {
            var product = await _productService.CreateAsync(model);
            if (product != null)
            {
                await _productService.UploadImageAsync(product, model.Image);
            }
            ModelState.AddModelError("", "You added product succesfully!");
        }

        ModelState.AddModelError("", "Something went wrong when trying to add product.");
        return View(model);
    }

}